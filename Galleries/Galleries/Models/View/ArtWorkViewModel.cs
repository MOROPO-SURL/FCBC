namespace MOROPO.FCBC.Galleries.Models.View
{
    public class ArtWorkViewModel
    {
        public System.Guid Id { get; set; }

        public string Title { get; set; }

        public ImageViewModel FeatureImage { get; set; }

        public string[] ArtTechniques { get; set; }

        public SGI.API.Common.Models.CreatorDisplayApiModel[] Creators { get; set; }

        public int Year { get; set; }
    }
}
