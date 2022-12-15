using Microsoft.AspNetCore.Mvc.Rendering;
using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Extensions;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CreationGroupInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<FBCB.SGI.Persistence.Entities.CreationGroup, System.Guid>
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

        public ICollection<CreationGroupArtisticManifestationBrigde> CreationGroupArtisticManifestationBrigdes { get; set; } = new List<CreationGroupArtisticManifestationBrigde>();

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        public System.Guid[] SelectedValues { get; set; }

        public ICollection<FBCB.SGI.Persistence.Entities.Artist> Members { get; set; }

        public ICollection<SelectListItem> NotMembers { get; set; } = new List<SelectListItem>();

        public FBCB.SGI.Persistence.Entities.CreationGroup Export()
        {
            var entity = new FBCB.SGI.Persistence.Entities.CreationGroup();

            Mergue(entity);

            return entity;
        }

        public void Import(FBCB.SGI.Persistence.Entities.CreationGroup entity)
        {
            Id = entity.Id;
            ImageId = entity.OrganizationIdentity.Image?.Id ?? System.Guid.Empty;
            ImageExtension = entity.OrganizationIdentity.Image?.ContentType.ContentTypeToExtension() ?? null;
            OrganizationIdentityName = entity.OrganizationIdentity.Name;
            CreationGroupArtisticManifestationBrigdes = entity.CreationGroupArtisticManifestationBrigdes;
            SelectedValues = entity.SelectedValues;
        }

        public void Mergue(FBCB.SGI.Persistence.Entities.CreationGroup entity)
        {
            entity.OrganizationIdentity.Name = OrganizationIdentityName;
            entity.CreationGroupArtisticManifestationBrigdes = CreationGroupArtisticManifestationBrigdes;
            entity.SelectedValues = SelectedValues;
        }

        public string GetExtension(FBCB.SGI.Persistence.Entities.Artist artist)
        {
            return artist.PersonIdentity.Image.ContentType.ContentTypeToExtension();
        }
    }
}
