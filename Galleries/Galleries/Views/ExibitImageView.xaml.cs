namespace MOROPO.FCBC.Galleries.Views
{
    public partial class ExibitImageView : Xamarin.Forms.ContentView
    {
        public ExibitImageView(Models.View.ImageViewModel model)
        {
            InitializeComponent();

            ImageView.Source = Xamarin.Forms.ImageSource.FromUri(new System.Uri(string.Format("{0}media/images/{1}.{2}", Constants.Networking.SGI_BASE_URL, model.Id, model.Extension)));
        }
    }
}