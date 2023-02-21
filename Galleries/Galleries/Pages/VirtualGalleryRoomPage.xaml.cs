namespace MOROPO.FCBC.Galleries.Pages
{
    public partial class VirtualGalleryRoomPage : Xamarin.Forms.ContentPage
    {
        public VirtualGalleryRoomPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await MainUrhoSurface.Show<Engine3D.VirtualGalleryRoomPresenter>(new Urho.ApplicationOptions()
            {
                Orientation = Urho.ApplicationOptions.OrientationType.LandscapeAndPortrait
            });
        }
    }
}