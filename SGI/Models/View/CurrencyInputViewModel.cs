using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Persistence.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CurrencyInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Currency, int>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Symbol")]
        public string Symbol { get; set; }
       
        [Display(Name = "Is Spaced Symbol")]
        public bool IsSpacedSymbol { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Position")]
        public CurrencySymbolPosition Position { get; set; }

        public Currency Export()
        {
            var entity = new Currency();

            Mergue(entity);

            return entity;
        }

        public void Import(Currency entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Symbol = entity.Symbol;
            IsSpacedSymbol = entity.IsSpacedSymbol;
            Position = entity.Position;
        }

        public void Mergue(Currency entity)
        {
            entity.Name = Name;
            entity.Symbol = Symbol;
            entity.IsSpacedSymbol = IsSpacedSymbol;
            entity.Position = Position;
        }
    }
}
