using MOROPO.FCBC.SGI.Persistence.Entities;
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

        public EmployeeAdministrativeRoleBridge Export()
        {
            var entity = new EmployeeAdministrativeRoleBridge();

            Mergue(entity);

            return entity;
        }

        public void Import(EmployeeAdministrativeRoleBridge entity)
        {
            EmployeeId = entity.EmployeeId;
            Employee = entity.Employee;
            AdministrativeRoleId = entity.RoleId;
            AdministrativeRole = entity.Role;
        }

        public void Mergue(EmployeeAdministrativeRoleBridge entity)
        {
            entity.EmployeeId = EmployeeId;
            entity.Employee = Employee;
            entity.RoleId = AdministrativeRoleId;
            entity.Role = AdministrativeRole;
        }
    }
}
