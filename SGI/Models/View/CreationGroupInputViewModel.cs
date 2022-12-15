using Microsoft.AspNetCore.Mvc.Rendering;
using MOROPO.FCBC.SGI.Extensions;
using MOROPO.FCBC.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CreationGroupInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<CreationGroup, System.Guid>
    {
        public System.Guid Id { get; set; }

        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [FormFileMaxLength(Length = 3145728, ErrorMessage = "FileLimitSizeExcededMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        public bool ResetImage { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Organization")]
        public string OrganizationIdentityName { get; set; }

        [Display(Name = "Select Artistict Manifestation")]
        public MultiSelectList ArtisticManifestations { get; set; }

        public ICollection<CreationGroupArtisticManifestationBridge> CreationGroupArtisticManifestationBrigdes { get; set; } = new List<CreationGroupArtisticManifestationBridge>();

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        public System.Guid[] SelectedValues { get; set; }

        public CreationGroup Export()
        {
            var entity = new CreationGroup();

            Mergue(entity);

            return entity;
        }

        public void Import(CreationGroup entity)
        {
            Id = entity.Id;
            ImageId = entity.Identity.Image?.Id ?? System.Guid.Empty;
            ImageExtension = entity.Identity.Image?.ContentType.ContentTypeToExtension() ?? null;
            OrganizationIdentityName = entity.Identity.Name;
            CreationGroupArtisticManifestationBrigdes = entity.ArtisticManifestationBrigdes;
        }

        public void Mergue(CreationGroup entity)
        {
            entity.Identity.Name = OrganizationIdentityName;
            entity.ArtisticManifestationBrigdes = CreationGroupArtisticManifestationBrigdes;
        }
    }
}
