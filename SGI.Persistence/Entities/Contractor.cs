namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class Contractor : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("IdentityId")]
        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities.OrganizationIdentity Identity { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid IdentityId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ContractorKindId")]
        public ContractorKind ContractorKind { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public int ContractorKindId { get; set; }

    }
}
