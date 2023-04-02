using MOROPO.FCBC.Galleries.Presenters;

namespace MOROPO.FCBC.Galleries.Pages
{
    public partial class ExposureRoomPage : SmartSolucionesCuba.XFToolKit.Mvvm.Pages.ContentPage
    {
        private readonly System.Guid exposureId;

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

                        await MainUrhoSurface.Show<VirtualGalleryRoomPresenter>(new Urho.ApplicationOptions("Assets")
                        {
                            Orientation = Urho.ApplicationOptions.OrientationType.LandscapeAndPortrait,
                        });
                    });                    
                }
            });
        }
    }
}