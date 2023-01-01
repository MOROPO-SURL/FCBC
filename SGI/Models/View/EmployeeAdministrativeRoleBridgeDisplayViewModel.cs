using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class EmployeeAdministrativeRoleBridgeDisplayViewModel
    {
        public System.Guid EmployeeId { get; set; }

        [Display(Name = "Trabajador")]
        public EmployeeDisplayViewModel Employee { get; set; }

        public System.Guid AdministrativeRoleId { get; set; }

        [Display(Name = "Rol Asignado")]
        public AdministrativeRoleDisplayViewModel AdministrativeRole { get; set; }

        public void Import(EmployeeAdministrativeRoleBridge entity)
        {
            EmployeeId = entity.EmployeeId;
            AdministrativeRoleId = entity.RoleId;

            Employee.Import(entity.Employee);
            AdministrativeRole.Import(entity.Role);
        }
    }
}
