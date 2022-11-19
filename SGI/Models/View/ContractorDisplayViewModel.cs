using MOROPO.FBCB.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ContractorDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Contractor, System.Guid>
    {
        public System.Guid Id { get; set; }

        public OrganizationIdentity Identity { get; set; }
        
        public ContractorKind ContractKind { get; set; }

        public void Import(Contractor entity)
        {
            Id = entity.Id;
            Identity = entity.Identity;
            ContractKind = ContractKind;
        }
    }
}
