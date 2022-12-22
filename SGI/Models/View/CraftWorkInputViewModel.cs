using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CraftWorkInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<MOROPO.FCBC.SGI.Persistence.Entities.CraftWork, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [System.ComponentModel.DataAnnotations.Display(Name = "Manifestation")]
        public System.Guid? ManifestationId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableManifestations { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        [Display(Name = "Price")]
        public decimal? Price { get; set; }

        [Display(Name = "PriceCurrencyId")]
        public int? PriceCurrencyId { get; set; }

        [Display(Name = "Cost")]
        public decimal? Cost { get; set; }

        [Display(Name = "CostCurrencyId")]
        public int? CostCurrencyId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableCurrencies { get; set; }

        public CraftWork Export()
        {
            var entity = new CraftWork() ;

            Mergue(entity);

            return entity;
        }

        public void Import(CraftWork entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            ManifestationId = entity.ManifestationId;
            Price = entity.Price;
            PriceCurrencyId = entity.PriceCurrencyId;
            Cost = entity.Cost;
            CostCurrencyId = entity.CostCurrencyId;
            About = entity.About;
        }

        public void Mergue(CraftWork entity)
        {
            entity.Id = Id;
            entity.Title = Title;
            entity.ManifestationId = ManifestationId.Value;
            entity.Price = Price;
            entity.PriceCurrencyId = PriceCurrencyId;
            entity.Cost = Cost;
            entity.CostCurrencyId = CostCurrencyId;
            entity.About = About;
        }
    }
}
