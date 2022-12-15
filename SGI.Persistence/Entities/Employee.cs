using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class Employee: SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("PersonIdentityId")]
        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity PersonIdentity { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid PersonIdentityId { get; set; }

        public ICollection<EmployeeAdministrativeRoleBridge> EmployeeAdministrativeRoleBridges { get; set; }

        public Employee()
        {
            Id = System.Guid.NewGuid();
            PersonIdentity = new SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity();
            EmployeeAdministrativeRoleBridges = new List<EmployeeAdministrativeRoleBridge>();
        }
    }
}
