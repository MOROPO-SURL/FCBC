using System.Collections.Generic;

namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class AdministrativeRole: SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public AdministrativeRole()
        {
            Id = System.Guid.NewGuid();
            EmployeeAdministrativeRoleBridges = new List<EmployeeAdministrativeRoleBridge>();
        }

        public ICollection<EmployeeAdministrativeRoleBridge> EmployeeAdministrativeRoleBridges { get; set; }
    }
}
