using Microsoft.AspNetCore.Mvc.Rendering;
using MOROPO.FCBC.SGI.Extensions;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Core.Common.Models.View;
using MOROPO.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class EmployeeInputViewModel: SAPRESSC.Core.Persistence.Models.IEntityInputModel<Employee, System.Guid>
    {
        public System.Guid Id { get; set; }

        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [FormFileMaxLength(Length = 3145728, ErrorMessage = "FileLimitSizeExcededMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }

        public System.Guid ImageId { get; set; }

        public string ImageExtension { get; set; }

        public bool ResetImage { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Display(Name = "BornDate")]
        public System.DateTime? BornDate { get; set; }

        [Display(Name = "Sex")]
        public SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex Sex { get; set; }

        [Display(Name = "CivilStatus")]
        public SAPRESSC.Middleware.Identity.Persistence.Enumerations.CivilStatus CivilStatus { get; set; }

        [Display(Name = "Alias")]
        public string Alias { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }  

        [Display(Name = "Roles Administrativos")]
        public List<SelectionDescriptedItemInputViewModel<System.Guid>> SelectableRoles { get; set; }

        [Display(Name = "Address")]
        public string AddressPostal { get; set; }


        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "State")]
        public System.Guid StateId { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Municipality")]
        public System.Guid MunicipalityId { get; set; }

        public State State { get; set; }

        public Municipality Municipality { get; set; }

        public ICollection<EmployeeAdministrativeRoleBridge> EmployeeAdministrativeRoleBridges { get; set; } = new List<EmployeeAdministrativeRoleBridge>();

        public ICollection<SelectListItem> States { get; set; } = new List<SelectListItem>();

        public ICollection<SelectListItem> Municipalities { get; set; } = new List<SelectListItem>();

        public EmployeeInputViewModel()
        {
            BornDate = System.DateTime.Now.Date.AddYears(-16);
        }

        public Employee Export()
        {
            var entity = new Employee();

            Mergue(entity);

            return entity;
        }

        public void Import(Employee entity)
        {
            Id = entity.Id;
            ImageId = entity.Identity.Image?.Id ?? System.Guid.Empty;
            ImageExtension = entity.Identity.Image?.ContentType.ContentTypeToExtension() ?? null;
            Name = entity.Identity.Name;
            LastName = entity.Identity.LastName;
            BornDate = entity.Identity.BornDate;
            Sex = entity.Identity.Sex;
            CivilStatus = entity.Identity.CivilStatus;
            Alias = entity.Identity.Alias;
            About = entity.Identity.About;
            EmployeeAdministrativeRoleBridges = entity.AdministrativeRoleBridges;
            AddressPostal = entity.Identity.PersonPostalContactItemBridges.Select(b => b.ContactItem.Address).First();
            var region = entity.Identity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.Region;
            MunicipalityId = (System.Guid)((MunicipalityRegion)region).MunicipalityId;            
        }

        public void Mergue(Employee entity)
        {
            entity.Identity.Name = Name;
            entity.Identity.LastName = LastName;
            entity.Identity.BornDate = BornDate;
            entity.Identity.Sex = Sex;
            entity.Identity.CivilStatus = CivilStatus;
            entity.Identity.Alias = Alias;
            entity.Identity.About = About;
            entity.AdministrativeRoleBridges = entity.AdministrativeRoleBridges;
        }
    }
}
