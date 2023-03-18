namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtWorkImagesDisplayViewModel
    {
        public ArtWorkDisplayViewModel Work { get; set; }

        public ImageDisplayViewModel FeatureImage { get; set; }

        public ImageDisplayViewModel[] Images { get; set; }
    }
}