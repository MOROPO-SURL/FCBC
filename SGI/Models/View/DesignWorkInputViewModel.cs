using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class DesignWorkInputViewModel: SAPRESSC.Core.Persistence.Models.IEntityInputModel<MOROPO.FCBC.SGI.Persistence.Entities.DesignWork, System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [System.ComponentModel.DataAnnotations.Display(Name = "Title")]
        public string Title { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [System.ComponentModel.DataAnnotations.Display(Name = "Manifestation")]
        public System.Guid? ManifestationId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableManifestations { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "About")]
        public string About { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Price")]
        public decimal? Price { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "PriceCurrencyId")]
        public int? PriceCurrencyId { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Cost")]
        public decimal? Cost { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "CostCurrencyId")]
        public int? CostCurrencyId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableCurrencies { get; set; }

        public DesignWork Export()
        {
            var entity = new DesignWork();

            Mergue(entity);

            return entity;
        }

        public void Import(DesignWork entity)
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

        public void Mergue(DesignWork entity)
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
