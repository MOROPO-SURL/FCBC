using MOROPO.FCBC.SGI.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CraftWorkDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.CraftWork, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Fecha de Creación")]
        public int BornYear { get; set; }

        [Display(Name = "Price")]
        public decimal? Price { get; set; }
        public string PriceSymbol { get; set; }
        public Persistence.Entities.Currency PriceCurrency { get; set; }

        [Display(Name = "Cost")]
        public decimal? Cost { get; set; }
        public string CostSymbol { get; set; }
        public Persistence.Entities.Currency CostCurrency { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        [Display(Name = "CraftManifestation")]
        public CraftManifestationDisplayViewModel Manifestation { get; set; }

        public System.Guid? FeatureImageId { get; set; }

        public string FeatureImageExtension { get; set; }

        public SAPRESSC.Middleware.Media.Persistence.Entities.Image[] Images { get; set; }

        public CraftTechniqueDisplayViewModel[] Techniques { get; set; }

        public Persistence.Entities.CraftWorkAward[] Awards { get; set; }

        public CriterialDisplayViewModel[] Valorations { get; set; }

        public void Import(Persistence.Entities.CraftWork entity)
        {
            Id = entity.Id;            
            Title = entity.Title;
            Manifestation = new CraftManifestationDisplayViewModel();
            FeatureImageId = entity.FeatureImageId;
            FeatureImageExtension = entity.FeatureImage?.ContentType.ContentTypeToExtension();
            Images = entity.ImageBridges.Select(bridge => bridge.Image).ToArray();
            Price = entity.Price;
            Cost = entity.Cost;
            PriceSymbol = entity.PriceCurrency.Symbol;
            CostSymbol = entity.CostCurrency.Symbol;
            CostCurrency = entity.CostCurrency;
            PriceCurrency = entity.PriceCurrency;
            BornYear = entity.BornYear;
            About = entity.About;
            Techniques = entity.TechniqueBridges.Select(bridge =>
            {
                var model = new CraftTechniqueDisplayViewModel();

                model.Import(bridge.Technique);

                return model;
            }).ToArray();
            Awards = entity.Awards.ToArray();
            Manifestation.Import(entity.Manifestation);
        }
    }
}
