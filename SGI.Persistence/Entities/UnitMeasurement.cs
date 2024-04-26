namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class UnitMeasurement : SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set ; }

        public string Symbol { get; set; }

        public decimal? MilimetersRatio { get; set; }

        public decimal? GramsRatio { get; set; }

        public UnitMeasurement()
        {
            Id = System.Guid.NewGuid();
        }

    }
}
