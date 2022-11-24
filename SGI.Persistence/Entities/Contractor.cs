namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class Contractor : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public Contractor()
        {
            Id = System.Guid.NewGuid();
            OrganizationIdentity = new SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities.OrganizationIdentity();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("OrganizationIdentityId")]
        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities.OrganizationIdentity OrganizationIdentity { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid OrganizationIdentityId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ContractorKindId")]
        public ContractorKind ContractorKind { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public int ContractorKindId { get; set; }

    }
}
