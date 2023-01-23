using MOROPO.FCBC.SGI.Persistence.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtWorkDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.ArtWork, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Fecha de Creación")]
        public int BornYear { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        public string Collection { get; set; }

        public string Location { get; set; }

        public decimal? Price { get; set; }       
        public string PriceSymbol { get; set; }       

        public decimal? Cost { get; set; }        
        public string CostSymbol { get; set; }        

        public ArtisticManifestationDisplayViewModel Manifestation { get; set; }

        public SAPRESSC.Middleware.Media.Persistence.Entities.Image FeatureImage { get; set; }

        public SAPRESSC.Middleware.Media.Persistence.Entities.Image[] Images { get; set; }

        public ArtisticTechniqueDisplayViewModel[] Techniques { get; set; }

        public ICollection<ArtWorkAward> Awards { get; set; }

        public void Import(Persistence.Entities.ArtWork entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            BornYear = entity.BornYear;
            About = entity.About;
            Collection = entity.Collection;
            Location = entity.Location;
            Price = entity.Price;
            Cost = entity.Cost;
            CostSymbol = entity.CostCurrency.Symbol;
            PriceSymbol = entity.PriceCurrency.Symbol;
            Manifestation = new ArtisticManifestationDisplayViewModel();
            FeatureImage = entity.FeatureImage;
            Images = entity.ImageBridges.Select(bridge => bridge.Image).ToArray();
            Techniques = entity.TechniqueBridges.Select(bridge =>
            {
                var model = new ArtisticTechniqueDisplayViewModel();

                model.Import(bridge.Technique);

                return model;
            }).ToArray();
            Awards = entity.Awards;

            Manifestation.Import(entity.Manifestation);
        }
    }
}
