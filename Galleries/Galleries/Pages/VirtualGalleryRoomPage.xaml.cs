namespace MOROPO.FCBC.Galleries.Pages
{
    public partial class VirtualGalleryRoomPage : Xamarin.Forms.ContentPage
    {
        public VirtualGalleryRoomPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1)).ContinueWith(task =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    MainActivityIndicator.IsRunning = false;
                    MainActivityIndicator.IsVisible = false;

                    await MainUrhoSurface.Show<Engine3D.VirtualGalleryRoomPresenter>(new Urho.ApplicationOptions("Assets")
                    {
                        Orientation = Urho.ApplicationOptions.OrientationType.LandscapeAndPortrait,
                        TouchEmulation = true
                    });
                });
            });
        }
    }
}