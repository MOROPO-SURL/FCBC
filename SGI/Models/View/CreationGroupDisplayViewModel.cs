using MOROPO.FCBC.SGI.Extensions;
using MOROPO.FCBC.SGI.Persistence.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CreationGroupDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<CreationGroup, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        [Display(Name = "Organization")]
        public string OrganizationIdentityName { get; set; }

        public ICollection<CreationGroupArtisticManifestationBridge> CreationGroupArtisticManifestationBrigdes { get; set; } = new List<CreationGroupArtisticManifestationBridge>();
        public ICollection<CreationGroupAward> CreationGroupAwards { get; set; } = new List<CreationGroupAward>();

        public void Import(CreationGroup entity)
        {
            Id = entity.Id;
            ImageId = entity.Identity.ImageId ?? System.Guid.Empty;
            ImageExtension = entity.Identity.Image?.ContentType.ContentTypeToExtension() ?? null;
            OrganizationIdentityName = entity.Identity.Name;
            CreationGroupArtisticManifestationBrigdes = entity.ArtisticManifestationBrigdes;
            CreationGroupAwards = entity.CreationGroupAwards;
        }
    }
}
