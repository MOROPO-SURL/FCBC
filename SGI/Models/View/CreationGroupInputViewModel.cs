using Microsoft.AspNetCore.Mvc.Rendering;
using MOROPO.FCBC.SGI.Extensions;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CreationGroupInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<CreationGroup, System.Guid>
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
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "RegistrationNumber")]
        public int RegistrationNumber { get; set; }

        [Display(Name = "RegistratedAt")]
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        public System.DateTime RegistratedAt { get; set; }

        [Display(Name = "RegistrationEndAt")]
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        public System.DateTime RegistrationEndAt { get; set; }

        [Display(Name = "Agreement")]
        public System.Guid? AgreementId { get; set; }

        [Display(Name = "CreatedAt")]
        public System.DateTime? CreatedAt { get; set; }

        public ICollection<SelectListItem> AvailableAgreements { get; set; } = new List<SelectListItem>();

        public ICollection<CreationGroupArtisticManifestationBridge> CreationGroupArtisticManifestationBrigdes { get; set; } = new List<CreationGroupArtisticManifestationBridge>();
        public ICollection<ArtistDisplayViewModel> Members { get; set; }

        public ICollection<SelectListItem> NotMembers { get; set; } = new List<SelectListItem>();

        [Display(Name = "About")]
        public string About { get; set; }

        public List<SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>> AwardsToSelect { get; set; } = new List<SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>>();
        public List<SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>> RecognitionsToSelect { get; set; } = new List<SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>>();
        public List<SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>> DistintionsToSelect { get; set; } = new List<SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>>();


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
            Name = entity.Identity.Name;
            Brand = entity.Brand;
            About = entity.Identity.About;
            CreationGroupArtisticManifestationBrigdes = entity.ArtisticManifestationBrigdes;
            CreatedAt = entity.Identity.BornDate;
            RegistrationNumber = entity.RegistrationNumber;
            RegistratedAt = entity.RegistratedAt;
            RegistrationEndAt = entity.RegistrationEndAt;
            AgreementId = entity.AgreementId;
        }

        public void Mergue(CreationGroup entity)
        {
            entity.Identity.Name = Name;
            entity.Identity.About = About;
            entity.Brand = Brand;
            entity.ArtisticManifestationBrigdes = CreationGroupArtisticManifestationBrigdes;
            entity.RegistrationNumber = RegistrationNumber;
            entity.RegistratedAt = RegistratedAt;
            entity.RegistrationEndAt = RegistrationEndAt;
            entity.Identity.BornDate = CreatedAt;
        }

        //public string GetExtension(Artist artist)
        //{
        //    return artist.Identity.Image.ContentType.ContentTypeToExtension();
        //}
    }
}
