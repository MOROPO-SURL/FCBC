namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class Contractor : SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public Contractor()
        {
            Id = System.Guid.NewGuid();
            Identity = new SAPRESSC.Middleware.Identity.Persistence.Entities.OrganizationIdentity();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("IdentityId")]
        public SAPRESSC.Middleware.Identity.Persistence.Entities.OrganizationIdentity Identity { get; set; }
        public System.Guid IdentityId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ContractorKindId")]
        public ContractorKind ContractorKind { get; set; }
        public int ContractorKindId { get; set; }

    }
}
