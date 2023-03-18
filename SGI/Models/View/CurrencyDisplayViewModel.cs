using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Persistence.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CurrencyDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Currency, int>
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Symbol")]
        public string Symbol { get; set; }

        [Display(Name = "Is Spaced Symbol")]
        public bool IsSpacedSymbol { get; set; }

        [Display(Name = "Position")]
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
