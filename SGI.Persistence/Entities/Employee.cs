namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class Employee : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("IdentityId")]
        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity Identity { get; set; }
        public System.Guid IdentityId { get; set; }

        public System.Collections.Generic.ICollection<EmployeeAdministrativeRoleBridge> AdministrativeRoleBridges { get; set; }

        public Employee()
        {
            Id = System.Guid.NewGuid();
            Identity = new SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity();
            AdministrativeRoleBridges = new System.Collections.Generic.List<EmployeeAdministrativeRoleBridge>();
        }
    }
}
