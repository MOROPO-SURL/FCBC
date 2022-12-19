namespace MOROPO.FCBC.SGI.Persistence.Entities.Contract
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public abstract class AbstractWork : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Title { get; set; }

        public int BornYear { get; set; }

        public string About { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("FeatureImageId")]
        public SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities.Image FeatureImage { get; set; }
        public System.Guid? FeatureImageId { get; set; }

        public decimal? Price { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("PriceCurrencyId")]
        public Currency PriceCurrency { get; set; }
        public int? PriceCurrencyId { get; set; }

        public decimal? Cost { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("CostCurrencyId")]
        public Currency CostCurrency { get; set; }
        public int? CostCurrencyId { get; set; }
    }
}
