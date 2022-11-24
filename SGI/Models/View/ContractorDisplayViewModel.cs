using MOROPO.FBCB.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ContractorDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Contractor, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ContractKindName { get; set; }

        public string OrganizationIdentityName { get; set; }        

        public void Import(Contractor entity)
        {
            Id = entity.Id;
            ImageId = entity.OrganizationIdentity.ImageId ?? System.Guid.Empty; ;
            ContractKindName = entity.ContractorKind.Name;
            OrganizationIdentityName = entity.OrganizationIdentity.Name;            
        }
    }
}
