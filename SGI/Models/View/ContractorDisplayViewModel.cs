using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Extensions;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ContractorDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Contractor, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        public string ContractKindName { get; set; }

        public string OrganizationIdentityName { get; set; }        

        public void Import(Contractor entity)
        {
            Id = entity.Id;
            ImageId = entity.OrganizationIdentity.ImageId ?? System.Guid.Empty;
            ImageExtension = entity.OrganizationIdentity.Image?.ContentType.ContentTypeToExtension() ?? null;
            ContractKindName = entity.ContractorKind.Name;
            OrganizationIdentityName = entity.OrganizationIdentity.Name;            
        }
    }
}
