using MOROPO.FCBC.Galleries.Presenters;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace MOROPO.FCBC.Galleries.Pages
{
    public partial class ExposureRoomPage : SmartSolucionesCuba.XFToolKit.Mvvm.Pages.BindingContextedContentPage<Models.Page.ExposureRoomPageModel>
    {
        private readonly System.Guid exposureId;

        private VirtualGalleryRoomPresenter virtualRoomPresenter;

        public ExposureRoomPage(System.Guid exposureId, Urho.Color? frameBaseColor) : base(new Models.Page.ExposureRoomPageModel { IsBusy = true })
        {
            this.exposureId = exposureId;

            Providers.IsolateExposureDataProvider.Instance.FrameBaseColor = frameBaseColor;

            InitializeComponent();

            IsEnabled = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            IsEnabled = true;

            var exhibitsApiConnectClient = new SGI.API.Connect.Clients.ExhibitConsultApiConnectClient(Providers.HttpClientHelperProvider.Instance)
            {
                Culture = AppResources.Culture
            };

            exhibitsApiConnectClient.FilterAsync(new SGI.API.Common.Models.Filter.ExhibitFilterApiModel { ExposureId = exposureId }).ContinueWith(async task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() => { Helpers.ExceptionHandlingHelper.Instance.DefaultHandle(task.Exception); });
                }
                else
                {
                    await Providers.IsolateExposureDataProvider.Instance.PrepareExhibitsAsync(task.Result);

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                    {
                        virtualRoomPresenter = await MainUrhoSurface.Show<VirtualGalleryRoomPresenter>(new Urho.ApplicationOptions("Assets")
                        {
                            Orientation = Urho.ApplicationOptions.OrientationType.Landscape,
                            NoSound = true,
                            ResizableWindow = Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.UWP || Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.GTK || Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.macOS,
                        });

                        BindingContextPageModel.IsBusy = false;

                        virtualRoomPresenter.ExposureRoomPageModel = BindingContextPageModel;
                    });                 
                }
            });
        }

        protected override bool OnBackButtonPressed()
        {
            if (BindingContextPageModel.TargetArtWork != null)
            {
                BindingContextPageModel.TargetArtWork = null;

                virtualRoomPresenter.BindSceneEvents();
            }
            else
            {
                DisplayAlert("Confirmar", "Está seguro que desea abandonar la sala?", "Abandonar", "Continuar").ContinueWith(task =>
                {
                    if (!task.Result) return;

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        virtualRoomPresenter?.Exit();

                        virtualRoomPresenter = null;

                        this.NavigationPopModalPageAsync();
                    });
                });
            }

            return true;
        }

        private void CloseButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            if (BindingContextPageModel.TargetArtWork == null) return;
            
            BindingContextPageModel.TargetArtWork = null;

            System.Threading.Tasks.Task.Delay(System.TimeSpan.FromMilliseconds(500)).ContinueWith(_ => Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                virtualRoomPresenter.BindSceneEvents();
            }));
        }

        private void ExibitDetailButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushModalPageAsync(new ExibitDetailPage(BindingContextPageModel.TargetArtWork)).ContinueWith(_ =>
            {
                BindingContextPageModel.TargetArtWork = null;

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    virtualRoomPresenter.BindSceneEvents();
                });
            });
        }
    }
}