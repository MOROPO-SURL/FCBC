using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class EmployeeAdministrativeRoleBridge
    {
        public System.Guid EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public System.Guid AdministrativeRoleId { get; set; }

        public AdministrativeRole AdministrativeRole { get; set; }
    }
}
