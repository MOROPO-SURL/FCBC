namespace MOROPO.FCBC.SGI.Models.View
{
    public class ServiceWorkImagesDisplayViewModel
    {
        public ServiceWorkDisplayViewModel Work { get; set; }

        public ImageDisplayViewModel FeatureImage { get; set; }

        public ImageDisplayViewModel[] Images { get; set; }
    }
}
