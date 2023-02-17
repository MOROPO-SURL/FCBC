using MOROPO.FCBC.SGI.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class DesignWorkDisplayViewModel: SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.DesignWork, System.Guid>
    {
        public Guid Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Fecha de Creación")]
        public int BornYear { get; set; }

        [Display(Name = "Price")]
        public decimal? Price { get; set; }
        public string PriceSymbol { get; set; }
        public Currency PriceCurrency { get; set; }

        [Display(Name = "Cost")]
        public decimal? Cost { get; set; }
        public string CostSymbol { get; set; }
        public Currency CostCurrency { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        [Display(Name = "DesignManifestation")]
        public DesignManifestationDisplayViewModel Manifestation { get; set; }

        public SAPRESSC.Middleware.Media.Persistence.Entities.Image FeatureImage { get; set; }

        public SAPRESSC.Middleware.Media.Persistence.Entities.Image[] Images { get; set; }

        public DesignTechniqueDisplayViewModel[] Techniques { get; set; }

        public System.Collections.Generic.ICollection<DesignWorkAward> Awards { get; set; }
        public ICollection<DesignWorkAssessmentBridge> DesignWorkAssessmentBridges { get; set; }

        public void Import(DesignWork entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Manifestation = new DesignManifestationDisplayViewModel();
            FeatureImage = entity.FeatureImage;
            Images = entity.ImageBridges.Select(bridge => bridge.Image).ToArray();
            BornYear = entity.BornYear;
            Price = entity.Price;
            PriceSymbol = entity.PriceCurrency.Symbol;
            Cost = entity.Cost;
            CostSymbol = entity.CostCurrency.Symbol;
            CostCurrency = entity.CostCurrency;
            PriceCurrency = entity.PriceCurrency;
            About = entity.About;
            Techniques = entity.TechniqueBridges.Select(bridge =>
            {
                var model = new DesignTechniqueDisplayViewModel();

                model.Import(bridge.Technique);

                return model;
            }).ToArray();
            Awards = entity.Awards;
            DesignWorkAssessmentBridges = entity.WorkAssessmentBridges;
            Manifestation.Import(entity.Manifestation);
        }
    }
}
