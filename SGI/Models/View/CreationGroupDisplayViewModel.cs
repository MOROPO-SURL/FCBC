using MOROPO.FCBC.SGI.Extensions;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CreationGroupDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<FBCB.SGI.Persistence.Entities.CreationGroup, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        [Display(Name = "Organization")]
        public string OrganizationIdentityName { get; set; }

        public void Import(FBCB.SGI.Persistence.Entities.CreationGroup entity)
        {
            Id = entity.Id;
            ImageId = entity.OrganizationIdentity.ImageId ?? System.Guid.Empty;
            ImageExtension = entity.OrganizationIdentity.Image?.ContentType.ContentTypeToExtension() ?? null;
            OrganizationIdentityName = entity.OrganizationIdentity.Name;
        }
    }
}
