using MOROPO.FCBC.SGI.API.Common.Models;

namespace MOROPO.FCBC.Galleries.Models.View
{
    public class ArtWorkViewModel : Contract.AbstractWorkViewModel
    {
        public string Collection { get; set; }

        public string Location { get; set; }

        public decimal? Profundity { get; set; }

        public decimal? Height { get; set; }

        public decimal? Width { get; set; }

        public UnitMeasurementDisplayApiModel DimensionsUnitMeasurement { get; set; }

        public decimal? Weight { get; set; }

        public UnitMeasurementDisplayApiModel WeightUnitMeasurement { get; set; }

        public ArtWorkAwardDisplayApiModel[] ArtWorkAwards { get; set; }
    }
}
