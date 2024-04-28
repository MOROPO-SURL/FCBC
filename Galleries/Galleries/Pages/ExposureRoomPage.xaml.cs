using MOROPO.FCBC.Galleries.Presenters;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace MOROPO.FCBC.Galleries.Pages
{
    public partial class ExposureRoomPage : SmartSolucionesCuba.XFToolKit.Mvvm.Pages.BindingContextedContentPage<Models.Page.ExposureRoomPageModel>
    {
        private readonly System.Guid exposureId;

        private VirtualGalleryRoomPresenter virtualRoomPresenter;
        private Models.VirtualGalleryPresentationStateModel virtualGalleryPresentationState;

        public ExposureRoomPage(System.Guid exposureId, Urho.Color? frameBaseColor) : base(new Models.Page.ExposureRoomPageModel { IsBusy = true })
        {
            this.exposureId = exposureId;

            Providers.IsolateExposureDataProvider.Instance.FrameBaseColor = frameBaseColor;

            InitializeComponent();

            IsEnabled = false;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled)
            {
                if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
                {
                    virtualRoomPresenter = await MainUrhoSurface.Show<VirtualGalleryRoomPresenter>(new Urho.ApplicationOptions("Assets")
                    {
                        Orientation = Urho.ApplicationOptions.OrientationType.LandscapeAndPortrait,
                        NoSound = true                        
                    });

                    virtualRoomPresenter.LoadSceneState(virtualGalleryPresentationState);

                    virtualRoomPresenter.ExposureRoomPageModel = BindingContextPageModel;

                    BindingContextPageModel.IsBusy = false;
                }

                return; 
            }

            IsEnabled = true;

            var exhibitsApiConnectClient = new SGI.API.Connect.Clients.ExhibitConsultApiConnectClient(Providers.HttpClientHelperProvider.Instance)
            {
                Culture = AppResources.Culture
            };

            await exhibitsApiConnectClient.FilterAsync(new SGI.API.Common.Models.Filter.ExhibitFilterApiModel { ExposureId = exposureId }).ContinueWith(async task =>
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

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (virtualRoomPresenter != null) return;

            Urho.Forms.UrhoSurface.OnDestroy();

            virtualRoomPresenter = null;
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
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                BindingContextPageModel.IsBusy = true;

                virtualGalleryPresentationState = virtualRoomPresenter.BuildSceneState();

                virtualRoomPresenter = null;
            }

            var target = BindingContextPageModel.TargetArtWork;

            BindingContextPageModel.TargetArtWork = null;

            System.Threading.Tasks.Task.Delay(300).ContinueWith(_ =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    this.NavigationPushModalPageAsync(new ExibitDetailPage(target)).ContinueWith(__ =>
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            if (Xamarin.Forms.Device.RuntimePlatform != Xamarin.Forms.Device.Android) virtualRoomPresenter?.BindSceneEvents();
                        });
                    });
                });
            });
        }
    }
}