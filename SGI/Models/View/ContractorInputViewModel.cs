using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Extensions;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ContractorInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<Contractor, System.Guid>
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

        
        [Display(Name = "Contract Type")]
        public string ContractKindName { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "ContractKindId")]
        public int? ContractKindId { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ContractKinds { get; set; }

        public Contractor Export()
        {
            var entity = new Contractor();

            Mergue(entity);

            return entity;
        }

        public void Import(FBCB.SGI.Persistence.Entities.Contractor entity)
        {
            Id = entity.Id;
            ImageId = entity.OrganizationIdentity.Image?.Id ?? System.Guid.Empty;
            ImageExtension = entity.OrganizationIdentity.Image?.ContentType.ContentTypeToExtension()??null;
            OrganizationIdentityName = entity.OrganizationIdentity.Name;
            ContractKindId = entity.ContractorKindId;                               
        }

        public void Mergue(FBCB.SGI.Persistence.Entities.Contractor entity)
        {            
            entity.OrganizationIdentity.Name = OrganizationIdentityName;
        }
    }
}
