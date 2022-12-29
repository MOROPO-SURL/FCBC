using Microsoft.AspNetCore.Mvc.Rendering;
using MOROPO.FCBC.SGI.Extensions;
using MOROPO.FCBC.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

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

        [Display(Name = "Número de registro del creador")]
        public int RegistrationNumber { get; set; }

        [Display(Name = "Fecha en que expira")]
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        public System.DateTime? RegistrationEndAt { get; set; }

        [Display(Name = "Resolution")]
        public System.Guid? ResolutionId { get; set; }

        [Display(Name = "Fecha de Creación")]
        public System.DateTime CreateDate { get; set; } = System.DateTime.Now.Date.AddYears(-16);

        public ICollection<SelectListItem> Resolutions { get; set; } = new List<SelectListItem>();

        public ICollection<CreationGroupArtisticManifestationBridge> CreationGroupArtisticManifestationBrigdes { get; set; } = new List<CreationGroupArtisticManifestationBridge>();

        public System.Guid[] SelectedValues { get; set; }

        public ICollection<ArtistDisplayViewModel> Members { get; set; }

        public ICollection<SelectListItem> NotMembers { get; set; } = new List<SelectListItem>();

        [Display(Name = "About")]
        public string About { get; set; }

        public List<SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>> AwardsToSelect { get; set; } = new List<SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>>();
        public List<SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>> RecognitionsToSelect { get; set; } = new List<SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>>();
        public List<SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>> DistintionsToSelect { get; set; } = new List<SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>>();


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
            About = entity.Identity.About;
            CreationGroupArtisticManifestationBrigdes = entity.ArtisticManifestationBrigdes;
            CreateDate = entity.Identity.BornDate;
            RegistrationNumber = entity.RegistrationNumber;
            RegistrationEndAt = entity.RegistrationEndAt;
            ResolutionId = entity.ResolutionId;
        }

        public void Mergue(CreationGroup entity)
        {
            entity.Identity.Name = OrganizationIdentityName;
            entity.Identity.About = About;
            entity.ArtisticManifestationBrigdes = CreationGroupArtisticManifestationBrigdes;
            entity.RegistrationNumber = RegistrationNumber;
            entity.RegistrationEndAt = RegistrationEndAt;
            entity.Identity.BornDate = CreateDate;
        }

        public string GetExtension(Artist artist)
        {
            return artist.Identity.Image.ContentType.ContentTypeToExtension();
        }
    }
}
