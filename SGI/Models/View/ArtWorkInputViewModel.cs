using MOROPO.FCBC.SGI.Extensions;
using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtWorkInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<ArtWork, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "BornYear")]
        public int BornYear { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Manifestation")]
        public System.Guid? ManifestationId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvaiableManifestations { get; set; }

        [Display(Name = "FeatureImage")]
        public System.Guid? FeatureImageId { get; set; }

        public string FeatureImageExtenion { get; set; }

        public SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities.Image[] AvailablImages { get; set; }

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
            AvailablImages = entity.ImageBridges.Select(bridge => bridge.Image).ToArray();
        }

        public void Mergue(ArtWork entity)
        {
            entity.Title = Title;
            entity.BornYear = BornYear;
            entity.About = About;
            entity.ManifestationId = ManifestationId.Value;
            entity.FeatureImageId = FeatureImageId;
        }
    }
}
