using MOROPO.FBCB.SGI.Persistence.Entities;
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

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Contractor Name")]
        public string ContractorName { get; set; }        

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Organization")]
        public string OrganizationIdentityName { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "OrganizationIdentityId")]
        public Guid? OrganizationIdentityId { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Contract Type")]
        public string ContractKindName { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "ContractKind")]
        public int? ContractKindId { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> OrgaizacionIdentities { get; set; }
        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ContractKinds { get; set; }

        public Contractor Export()
        {
            var entity = new Contractor();

            Mergue(entity);

            return entity;
        }

        public void Import(Contractor entity)
        {
            Id = entity.Id;
            OrganizationIdentityId = entity.OrganizationIdentityId;
            OrganizationIdentityName = entity.OrganizationIdentity.Name;
            ContractKindId = entity.ContractorKindId;
            ContractKindName = entity.ContractorKind.Name;
            ImageId = entity.OrganizationIdentity.Image?.Id ?? System.Guid.Empty; ;            
        }

        public void Mergue(Contractor entity)
        {            
            entity.OrganizationIdentity.Name = OrganizationIdentityName;            
            entity.ContractorKind.Name = ContractKindName;            
        }
    }
}
