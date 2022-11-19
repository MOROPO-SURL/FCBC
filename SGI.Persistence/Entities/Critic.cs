
namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class Critic : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("PersonIdentityId")]
        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity PersonIdentity { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid PersonIdentityId { get; set; }
    }
}
