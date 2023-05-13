namespace MOROPO.FCBC.Galleries.Models.Page
{
    public class ExposureRoomPageModel : SmartSolucionesCuba.XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private View.ArtWorkViewModel targetArtWork;

        public View.ArtWorkViewModel TargetArtWork { get => targetArtWork; set => SetProperty(ref targetArtWork, value); }
    }
}
