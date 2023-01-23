using MOROPO.FCBC.SGI.Extensions;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ContractorInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Contractor, System.Guid>
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

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "ContractKindId")]
        public int ContractKindId { get; set; }

        [Display(Name = "ContractKind")]
        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ContractKinds { get; set; }

        [Display(Name = "Founding date")]
        public System.DateTime? BornDate { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Address")]
        public string AddressPostal { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "State")]
        public System.Guid StateId { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Municipality")]
        public System.Guid MunicipalityId { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> States { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Municipalities { get; set; }


        public ContractorInputViewModel()
        {
            BornDate = System.DateTime.Now.Date.AddYears(-16);
        }

        public Contractor Export()
        {
            var entity = new Contractor();

            Mergue(entity);

            return entity;
        }

        public void Import(Contractor entity)
        {
            Id = entity.Id;
            ImageId = entity.Identity.Image?.Id ?? System.Guid.Empty;
            ImageExtension = entity.Identity.Image?.ContentType.ContentTypeToExtension()??null;
            OrganizationIdentityName = entity.Identity.Name;
            ContractKindId = entity.ContractorKindId;
            BornDate = entity.Identity.BornDate;
            About = entity.Identity.About;
            AddressPostal = entity.Identity.OrganizationPostalContactItemBridges.Select(b => b.ContactItem.Address).FirstOrDefault();
            var region = entity.Identity.OrganizationPostalContactItemBridges.FirstOrDefault().ContactItem.Region;
            MunicipalityId = (System.Guid)((MunicipalityRegion)region).MunicipalityId;
        }

        public void Mergue(Contractor entity)
        {            
            entity.Identity.Name = OrganizationIdentityName;
            entity.Identity.BornDate = BornDate;
            entity.Identity.About = About;
        }
    }
}
