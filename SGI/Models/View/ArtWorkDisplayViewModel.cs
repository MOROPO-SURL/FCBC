using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtWorkDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<ArtWork, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Fecha de Creación")]
        public int BornYear { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        [Display(Name = "Collection")]
        public string Collection { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Price")]
        public decimal? Price { get; set; }       
        public string PriceSymbol { get; set; }   
        public Currency PriceCurrency { get; set; }

        [Display(Name = "Cost")]
        public decimal? Cost { get; set; }        
        public string CostSymbol { get; set; }  
        public Currency CostCurrency { get; set; }

        [Display(Name = "ArtisticManifestation")]
        public ArtisticManifestationDisplayViewModel Manifestation { get; set; }

        public System.Guid? FeatureImageId { get; set; }

        public string FeatureImageExtension { get; set; }

        public SAPRESSC.Middleware.Media.Persistence.Entities.Image[] Images { get; set; }

        public ArtisticTechniqueDisplayViewModel[] Techniques { get; set; }

        public ICollection<AwardReachDiplayViewModel> Awards { get; set; }

        [Display(Name = "Height")]
        public decimal? Height { get; set; }

        [Display(Name = "Width")]
        public decimal? Width { get; set; }

        [Display(Name = "Profundity")]
        public decimal? Profundity { get; set; }

        [Display(Name = "DimensionsUnitMeasurement")]
        public System.Guid? DimensionsUnitMeasurementId { get; set; }

        public UnitMeasurement UnitMeasurement { get; set; }

        [Display(Name = "WorkWeight")]
        public decimal? Weight { get; set; }

        [Display(Name = "WeightUnitMeasurement")]
        public System.Guid? WeightUnitMeasurementId { get; set; }

        public ValorationDisplayViewModel[] Valorations { get; set; }

        public void Import(ArtWork entity)
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
            CostCurrency = entity.CostCurrency;
            PriceCurrency = entity.PriceCurrency;
            Manifestation = new ArtisticManifestationDisplayViewModel();
            FeatureImageId = entity.FeatureImageId;
            FeatureImageExtension = entity.FeatureImage?.ContentType.ContentTypeToExtension();
            Images = entity.ImageBridges.Select(bridge => bridge.Image).ToArray();
            Techniques = entity.TechniqueBridges.Select(bridge =>
            {
                var model = new ArtisticTechniqueDisplayViewModel();

                model.Import(bridge.Technique);

                return model;
            }).ToArray();
            Awards = entity.Awards.Select(item =>
            {
                var model = new AwardReachDiplayViewModel
                {
                    Id = item.Id,
                    Award = new AwardDisplayViewModel(),
                    Context = item.Context
                };

                model.Award.Import(item.Award);

                return model;
            }).ToArray();
            Valorations = entity.Valorations.Select(item =>
            {
                var model = new ValorationDisplayViewModel
                {
                    Id = item.Id,
                    Critic = new CriticDisplayViewModel(),
                    Declaration = item.Declaration,
                    DeclaredAt = item.DeclaredAt
                };

                model.Critic.Import(item.Critic);

                return model;
            }).ToArray();
            Height = entity.Height;
            Width = entity.Width;
            Profundity = entity.Profundity;
            Weight = entity.Weight;
            UnitMeasurement = entity.DimensionsUnitMeasurement;
            Manifestation.Import(entity.Manifestation);            
        }
    }
}
