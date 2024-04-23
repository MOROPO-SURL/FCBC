namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class UnitMeasurementDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.UnitMeasurement, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public decimal? MilimetersRatio { get; set; }

        public void Import(Persistence.Entities.UnitMeasurement entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Symbol = entity.Symbol;
            MilimetersRatio = entity.MilimetersRatio;
        }
    }
}
