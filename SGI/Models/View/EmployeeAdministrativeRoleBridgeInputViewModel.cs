using MOROPO.FBCB.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class EmployeeAdministrativeRoleBridgeInputViewModel
    {
        public System.Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Trabajador")]
        public Employee Employee { get; set; }

        public System.Guid AdministrativeRoleId { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Rol Asignado")]
        public AdministrativeRole AdministrativeRole { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AdministrativeRoles { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Employees { get; set; }

        public FBCB.SGI.Persistence.Entities.EmployeeAdministrativeRoleBridge Export()
        {
            var entity = new FBCB.SGI.Persistence.Entities.EmployeeAdministrativeRoleBridge();

            Mergue(entity);

            return entity;
        }

        public void Import(FBCB.SGI.Persistence.Entities.EmployeeAdministrativeRoleBridge entity)
        {
            EmployeeId = entity.EmployeeId;
            Employee = entity.Employee;
            AdministrativeRoleId = entity.AdministrativeRoleId;
            AdministrativeRole = entity.AdministrativeRole;
        }

        public void Mergue(FBCB.SGI.Persistence.Entities.EmployeeAdministrativeRoleBridge entity)
        {
            entity.EmployeeId = EmployeeId;
            entity.Employee = Employee;
            entity.AdministrativeRoleId = AdministrativeRoleId;
            entity.AdministrativeRole = AdministrativeRole;
        }
    }
}
