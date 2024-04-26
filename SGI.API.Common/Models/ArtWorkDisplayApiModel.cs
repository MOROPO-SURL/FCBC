namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class ArtWorkDisplayApiModel : Contract.AbstractWorkDisplayApiModel<Persistence.Entities.ArtWork>
    {
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

        public ArtWorkAwardDisplayApiModel[] Awards { get; set; }

        public override void Import(Persistence.Entities.ArtWork entity)
        {
            base.Import(entity);

            Collection = entity.Collection;
            Location = entity.Location;
            Manifestation = new ArtWorkManifestationDisplayApiModel();
            Profundity = entity.Profundity;
            Height = entity.Height;
            Width = entity.Width;
            Weight = entity.Weight;

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
