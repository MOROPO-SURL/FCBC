using MOROPO.FCBC.SGI.Extensions;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class AssociationInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<FBCB.SGI.Persistence.Entities.Association, System.Guid>
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

        [Display(Name = "Founding date")]
        public System.DateTime BornDate { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

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

        public AssociationInputViewModel()
        {
            BornDate = System.DateTime.Now.Date.AddYears(-16);
        }
        public FBCB.SGI.Persistence.Entities.Association Export()
        {
            var entity = new FBCB.SGI.Persistence.Entities.Association();

            Mergue(entity);

            return entity;
        }

        public void Import(FBCB.SGI.Persistence.Entities.Association entity)
        {
            Id = entity.Id;
            ImageId = entity.OrganizationIdentity.Image?.Id ?? System.Guid.Empty;
            ImageExtension = entity.OrganizationIdentity.Image?.ContentType.ContentTypeToExtension() ?? null;
            OrganizationIdentityName = entity.OrganizationIdentity.Name;
            BornDate = entity.OrganizationIdentity.BornDate;
            About = entity.OrganizationIdentity.About;
            AddressPostal = entity.OrganizationIdentity.OrganizationPostalContactItemBridges.Select(b => b.ContactItem.Address).First();
            var region = entity.OrganizationIdentity.OrganizationPostalContactItemBridges.FirstOrDefault().ContactItem.Region;
            MunicipalityId = (System.Guid)((MunicipalityRegion)region).MunicipalityId;             
        }

        public void Mergue(FBCB.SGI.Persistence.Entities.Association entity)
        {
            entity.OrganizationIdentity.Name = OrganizationIdentityName;
            entity.OrganizationIdentity.BornDate = BornDate;
            entity.OrganizationIdentity.About = About;  
        }
    }
}
