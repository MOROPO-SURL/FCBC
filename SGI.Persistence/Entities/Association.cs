namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class Association : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("IdentityId")]
        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities.OrganizationIdentity Identity { get; set; }
        public System.Guid IdentityId { get; set; }

        public Association()
        {
            Id = System.Guid.NewGuid();
            Identity = new SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities.OrganizationIdentity();
        }
    }
}
