namespace MOROPO.FCBC.Galleries.Views
{
    public partial class ExibitVideoView : Xamarin.Forms.ContentView
    {
        public ExibitVideoView(SGI.API.Common.Models.VideoDisplayApiModel model)
        {
            InitializeComponent();

            VideoView.Source = Xamarin.CommunityToolkit.Core.MediaSource.FromUri(new System.Uri(string.Format("{0}media/attachments/{1}.{2}", Constants.Networking.SGI_BASE_URL, model.Id, model.Extension)));
        }
    }
}