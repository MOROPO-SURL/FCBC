using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Persistence.Enumerations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CurrencyDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Currency, int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public bool IsSpacedSymbol { get; set; }

        public CurrencySymbolPosition Position { get; set; }       

        public void Import(Currency entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Symbol = entity.Symbol;
            IsSpacedSymbol = entity.IsSpacedSymbol;
            Position = entity.Position;
        }
    }
}
