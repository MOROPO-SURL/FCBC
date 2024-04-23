namespace MOROPO.FCBC.SGI.Models.View
{
    public class UnitMeasurementDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.UnitMeasurement, System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Symbol")]
        public string Symbol { get; set; }

        public decimal? MilimetersRatio { get; set; }

        public decimal? GramsRatio { get; set; }

        public void Import(Persistence.Entities.UnitMeasurement entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Symbol = entity.Symbol;
            MilimetersRatio = entity.MilimetersRatio;
            GramsRatio = entity.GramsRatio;
        }
    }
}
