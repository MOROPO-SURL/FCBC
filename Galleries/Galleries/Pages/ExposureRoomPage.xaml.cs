using MOROPO.FCBC.Galleries.Presenters;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace MOROPO.FCBC.Galleries.Pages
{
    public partial class ExposureRoomPage : SmartSolucionesCuba.XFToolKit.Mvvm.Pages.ContentPage
    {
        private readonly System.Guid exposureId;

        private Urho.Application virtualRoomPresenter;

        public ExposureRoomPage(System.Guid exposureId)
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
                        MainActivityIndicator.IsRunning = false;
                        MainActivityIndicator.IsVisible = false;

                        virtualRoomPresenter = await MainUrhoSurface.Show<VirtualGalleryRoomPresenter>(new Urho.ApplicationOptions("Assets")
                        {
                            Orientation = Urho.ApplicationOptions.OrientationType.LandscapeAndPortrait,
                        });
                    });                    
                }
            });
        }

        protected override bool OnBackButtonPressed()
        {
            DisplayAlert("Confirmar", "Está seguro que desea abandonar la sala?", "Abandonar", "Continuar").ContinueWith(task =>
            {
                if (!task.Result) return;

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() => this.NavigationPopModalPageAsync());
            });

            return true;
        }

        protected override void DisposeUnmanagementResources()
        {
            base.DisposeUnmanagementResources();

            virtualRoomPresenter?.Exit();
            virtualRoomPresenter?.Dispose();

            virtualRoomPresenter = null;
        }
    }
}