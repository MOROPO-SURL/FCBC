using MOROPO.FCBC.SGI.Persistence.Enumerations;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class Currency : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.INamedEntity<int>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Symbol { get; set; }

        public bool IsSpacedSymbol { get; set; }

        public CurrencySymbolPosition Position { get; set; }

    }
}
