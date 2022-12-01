
namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class Association: SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("OrganizationIdentityId")]
        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities.OrganizationIdentity OrganizationIdentity { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid OrganizationIdentityId { get; set; }

        public Association()
        {
            Id = System.Guid.NewGuid();
            OrganizationIdentity = new SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities.OrganizationIdentity();
        }
    }
}
