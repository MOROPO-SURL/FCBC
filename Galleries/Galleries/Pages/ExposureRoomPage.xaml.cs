using MOROPO.FCBC.Galleries.Presenters;

namespace MOROPO.FCBC.Galleries.Pages
{
    public partial class ExposureRoomPage : SmartSolucionesCuba.XFToolKit.Mvvm.Pages.ContentPage
    {
        public ExposureRoomPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1)).ContinueWith(async task =>
            {
                try
                {
                    await Providers.ArtworkModelProvider.Instance.LoadAsync();

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                    {
                        MainActivityIndicator.IsRunning = false;
                        MainActivityIndicator.IsVisible = false;

                        await MainUrhoSurface.Show<VirtualGalleryRoomPresenter>(new Urho.ApplicationOptions("Assets")
                        {
                            Orientation = Urho.ApplicationOptions.OrientationType.LandscapeAndPortrait
                        });
                    });
                }
                catch (System.Exception e)
                {

                }
            });
        }
    }
}