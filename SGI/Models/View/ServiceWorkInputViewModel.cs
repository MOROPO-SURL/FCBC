namespace MOROPO.FCBC.SGI.Models.View
{
    public class ServiceWorkInputViewModel: SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<MOROPO.FCBC.SGI.Persistence.Entities.ServiceWork, System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [System.ComponentModel.DataAnnotations.Display(Name = "Title")]
        public string Title { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [System.ComponentModel.DataAnnotations.Display(Name = "Line")]
        public System.Guid? LineId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableLines { get; set; }

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

        public Persistence.Entities.ServiceWork Export()
        {
            var entity = new Persistence.Entities.ServiceWork();

            Mergue(entity);

            return entity;
        }        

        public void Import(Persistence.Entities.ServiceWork entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            LineId = entity.LineId;
            Price = entity.Price;
            PriceCurrencyId = entity.PriceCurrencyId;
            Cost = entity.Cost;
            CostCurrencyId = entity.CostCurrencyId;
            About = entity.About;
        }
       

        public void Mergue(Persistence.Entities.ServiceWork entity)
        {
            entity.Id = Id;
            entity.Title = Title;
            entity.LineId = LineId.Value;
            entity.Price = Price;
            entity.PriceCurrencyId = PriceCurrencyId;
            entity.Cost = Cost;
            entity.CostCurrencyId = CostCurrencyId;
            entity.About = About;
        }
        
    }
}
