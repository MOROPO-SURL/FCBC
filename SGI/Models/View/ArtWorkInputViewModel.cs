using MOROPO.FCBC.SGI.Extensions;
using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtWorkInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<ArtWork, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "CreationYear")]
        public int? BornYear { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        [Display(Name = "Collection")]
        public string Collection { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Price")]
        public decimal? Price { get; set; }

        [Display(Name = "PriceCurrencyId")]
        public int? PriceCurrencyId { get; set; }

        [Display(Name = "Cost")]
        public decimal? Cost { get; set; }

        [Display(Name = "CostCurrencyId")]
        public int? CostCurrencyId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableCurrencies { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "ArtisticManifestation")]
        public System.Guid? ManifestationId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableManifestations { get; set; }

        [Display(Name = "FeatureImage")]
        public System.Guid? FeatureImageId { get; set; }

        public string FeatureImageExtenion { get; set; }

        public SAPRESSC.Middleware.Media.Persistence.Entities.Image[] AvailableImages { get; set; }

        [Display(Name = "Height")]
        public decimal? Height { get; set; }

        [Display(Name = "Width")]
        public decimal? Width { get; set; }

        [Display(Name = "Profundity")]
        public decimal? Profundity { get; set; }

        [Display(Name = "DimensionsUnitMeasurement")]
        public System.Guid? DimensionsUnitMeasurementId { get; set; }

        [Display(Name = "WorkWeight")]
        public decimal? Weight { get; set; }

        [Display(Name = "WeightUnitMeasurement")]
        public System.Guid? WeightUnitMeasurementId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvaiableUnitMeasurements { get; set; }

        public ArtWork Export()
        {
            var entity = new ArtWork();

            Mergue(entity);

            return entity;
        }

        public void Import(ArtWork entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            BornYear = entity.BornYear;
            About = entity.About;
            ManifestationId = entity.ManifestationId;
            FeatureImageId = entity.FeatureImage?.Id;
            FeatureImageExtenion = entity.FeatureImage?.ContentType.ContentTypeToExtension();
            AvailableImages = entity.ImageBridges.Select(bridge => bridge.Image).ToArray();
            Collection = entity.Collection;
            Location = entity.Location;
            Price = entity.Price;
            PriceCurrencyId = entity.PriceCurrencyId;
            Cost = entity.Cost;
            CostCurrencyId = entity.CostCurrencyId;
            Height = entity.Height;
            Width = entity.Width;
            Profundity = entity.Profundity;
            DimensionsUnitMeasurementId = entity.DimensionsUnitMeasurementId;
            Weight = entity.Weight;
            WeightUnitMeasurementId = entity.WeightUnitMeasurementId;
        }

        public void Mergue(ArtWork entity)
        {
            entity.Title = Title;
            entity.BornYear = BornYear.Value;
            entity.About = About;
            entity.ManifestationId = ManifestationId.Value;
            entity.FeatureImageId = FeatureImageId;
            entity.Collection = Collection;
            entity.Location = Location;
            entity.Price = Price;
            entity.PriceCurrencyId = PriceCurrencyId;
            entity.Cost = Cost;
            entity.CostCurrencyId = CostCurrencyId;
            entity.Height = Height;
            entity.Width = Width;
            entity.Profundity = Profundity;
            entity.DimensionsUnitMeasurementId = DimensionsUnitMeasurementId;
            entity.Weight = Weight;
            entity.WeightUnitMeasurementId = WeightUnitMeasurementId;
        }
    }
}
