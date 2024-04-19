using MOROPO.FCBC.SGI.API.Common.Models;

namespace MOROPO.FCBC.Galleries.Models.View
{
    public class ArtWorkViewModel
    {
        public System.Guid Id { get; set; }

        public string Title { get; set; }

        public ImageViewModel FeatureImage { get; set; }

        public int Year { get; set; }

        public string About { get; set; }

        public string Collection { get; set; }

        public string Location { get; set; }

        public ArtWorkManifestationDisplayApiModel Manifestation { get; set; }

        public decimal? Profundity { get; set; }

        public decimal? Height { get; set; }

        public decimal? Width { get; set; }

        public UnitMeasurementDisplayApiModel DimensionsUnitMeasurement { get; set; }

        public decimal? Weight { get; set; }

        public UnitMeasurementDisplayApiModel WeightUnitMeasurement { get; set; }

        public ArtWorkTechniqueDisplayApiModel[] Techniques { get; set; }

        public CreatorDisplayApiModel[] Creators { get; set; }

        public ThemathicDisplayApiModel[] Themathics { get; set; }

        public MaterialDisplayApiModel[] Materials { get; set; }

        public ImageViewModel[] Images { get; set; }

        public VideoDisplayApiModel[] Videos { get; set; }

        public ArtWorkAwardDisplayApiModel[] ArtWorkAwards { get; set; }

        public ValorationDisplayApiModel[] Valorations { get; set; }
    }
}
