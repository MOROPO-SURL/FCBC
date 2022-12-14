using Microsoft.AspNetCore.Mvc.Rendering;
using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Extensions;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class EmployeeInputViewModel: SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<FBCB.SGI.Persistence.Entities.Employee, System.Guid>
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
        public System.DateTime BornDate { get; set; }

        [Display(Name = "Sex")]
        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex Sex { get; set; }

        [Display(Name = "CivilStatus")]
        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Enumerations.CivilStatus CivilStatus { get; set; }

        [Display(Name = "Alias")]
        public string Alias { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }
        
        [Display(Name = "Select Rol")]
        public MultiSelectList AdministrativeRoles { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        public System.Guid[] SelectedValues { get; set; }

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

        public FBCB.SGI.Persistence.Entities.Employee Export()
        {
            var entity = new FBCB.SGI.Persistence.Entities.Employee();

            Mergue(entity);

            return entity;
        }

        public void Import(FBCB.SGI.Persistence.Entities.Employee entity)
        {
            Id = entity.Id;
            ImageId = entity.PersonIdentity.Image?.Id ?? System.Guid.Empty;
            ImageExtension = entity.PersonIdentity.Image?.ContentType.ContentTypeToExtension() ?? null;
            Name = entity.PersonIdentity.Name;
            LastName = entity.PersonIdentity.LastName;
            BornDate = entity.PersonIdentity.BornDate;
            Sex = entity.PersonIdentity.Sex;
            CivilStatus = entity.PersonIdentity.CivilStatus;
            Alias = entity.PersonIdentity.Alias;
            About = entity.PersonIdentity.About;
            EmployeeAdministrativeRoleBridges = entity.EmployeeAdministrativeRoleBridges;
            SelectedValues = entity.SelectedValues;
            AddressPostal = entity.PersonIdentity.PersonPostalContactItemBridges.Select(b => b.ContactItem.Address).First();
            var region = entity.PersonIdentity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.Region;
            MunicipalityId = (System.Guid)((MunicipalityRegion)region).MunicipalityId;            
        }

        public void Mergue(FBCB.SGI.Persistence.Entities.Employee entity)
        {
            entity.PersonIdentity.Name = Name;
            entity.PersonIdentity.LastName = LastName;
            entity.PersonIdentity.BornDate = BornDate;
            entity.PersonIdentity.Sex = Sex;
            entity.PersonIdentity.CivilStatus = CivilStatus;
            entity.PersonIdentity.Alias = Alias;
            entity.PersonIdentity.About = About;
            entity.EmployeeAdministrativeRoleBridges = entity.EmployeeAdministrativeRoleBridges;
            entity.SelectedValues = SelectedValues;
        }
    }
}
