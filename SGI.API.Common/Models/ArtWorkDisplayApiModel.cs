namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class ArtWorkDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.ArtWork, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public string Collection { get; set; }

        public string Location { get; set; }

        public ImageDisplayApiModel FeatureImage { get; set; }

        public ArtWorkManifestationDisplayApiModel Manifestation { get; set; }

        public decimal? Profundity { get; set; }

        public decimal? Height { get; set; }

        public decimal? Width { get; set; }

        public UnitMeasurementDisplayApiModel DimensionsUnitMeasurement { get; set; }

        public decimal? Weight { get; set; }

        public UnitMeasurementDisplayApiModel WeightUnitMeasurement { get; set; }

        public string About { get; set; }

        public ArtWorkTechniqueDisplayApiModel[] ArtTechniques { get; set; }

        public CreatorDisplayApiModel[] Creators { get; set; }

        public ThemathicDisplayApiModel[] Themathics { get; set; }

        public MaterialDisplayApiModel[] Materials { get; set; }

        public ImageDisplayApiModel[] Images { get; set; }

        public VideoDisplayApiModel[] Videos { get; set; }

        public ArtWorkAwardDisplayApiModel[] ArtWorkAwards { get; set; }

        public ValorationDisplayApiModel[] Valorations { get; set; }

        public void Import(Persistence.Entities.ArtWork entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Year = entity.BornYear;
            Collection = entity.Collection;
            Location = entity.Location;
            Manifestation = new ArtWorkManifestationDisplayApiModel();
            Profundity = entity.Profundity;
            Height = entity.Height;
            Width = entity.Width;
            Weight = entity.Weight;
            About = entity.About;

            Manifestation.Import(entity.Manifestation);

            if (entity.DimensionsUnitMeasurement != null)
            {
                DimensionsUnitMeasurement = new UnitMeasurementDisplayApiModel();

                DimensionsUnitMeasurement.Import(entity.DimensionsUnitMeasurement);
            }

            if (entity.WeightUnitMeasurement != null)
            {
                WeightUnitMeasurement = new UnitMeasurementDisplayApiModel();

                WeightUnitMeasurement.Import(entity.WeightUnitMeasurement);
            }
        }
    }
}
