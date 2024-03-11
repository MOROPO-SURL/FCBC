using MOROPO.FCBC.Galleries.Presenters;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace MOROPO.FCBC.Galleries.Pages
{
    public partial class ExposureRoomPage : SmartSolucionesCuba.XFToolKit.Mvvm.Pages.BindingContextedContentPage<Models.Page.ExposureRoomPageModel>
    {
        private readonly System.Guid exposureId;

        private VirtualGalleryRoomPresenter virtualRoomPresenter;

        public ExposureRoomPage(System.Guid exposureId) : base(new Models.Page.ExposureRoomPageModel { IsBusy = true })
        {
            this.exposureId = exposureId;

            InitializeComponent();

            IsEnabled = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            IsEnabled = true;

            var exhibitsApiConnectClient = new SGI.API.Connect.Clients.ExhibitConsultApiConnectClient(Providers.HttpClientHelpeProvider.Instance);

            exhibitsApiConnectClient.FilterAsync(new SGI.API.Common.Models.Filter.ExhibitFilterApiModel { ExposureId = exposureId }).ContinueWith(async task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() => { Helpers.ExceptionHandlingHelper.Instance.DefaultHandle(task.Exception); });
                }
                else
                {
                    await Providers.ExhibitViewModelProvider.Instance.PrepareAsync(task.Result);

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                    {
                        virtualRoomPresenter = await MainUrhoSurface.Show<VirtualGalleryRoomPresenter>(new Urho.ApplicationOptions("Assets")
                        {
                            Orientation = Urho.ApplicationOptions.OrientationType.LandscapeAndPortrait
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

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() => this.NavigationPopModalPageAsync());
                });
            }

            return true;
        }

        protected override void DisposeUnmanagementResources()
        {
            base.DisposeUnmanagementResources();

            virtualRoomPresenter?.Exit();
            virtualRoomPresenter?.Dispose();

            virtualRoomPresenter = null;
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