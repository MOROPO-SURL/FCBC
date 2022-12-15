using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CreationGroup : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public CreationGroup()
        {
            Id = System.Guid.NewGuid();
            Identity = new SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities.OrganizationIdentity();
            Attachments = new List<CreationGroupAttachment>();
            ArtisticManifestationBrigdes = new List<CreationGroupArtisticManifestationBridge>();
            CraftManifestationBridges = new List<CreationGroupCraftManifestationBridge>();
            DesignManifestationBridges = new List<CreationGroupDesignManifestationBridge>();
            ServiceLineBridges = new List<CreationGroupServiceLineBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("IdentityId")]
        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities.OrganizationIdentity Identity { get; set; }
        public System.Guid IdentityId { get; set; }

        public ICollection<CreationGroupAttachment> Attachments { get; set; }

        public ICollection<CreationGroupArtisticManifestationBridge> ArtisticManifestationBrigdes { get; set; }

        public ICollection<CreationGroupCraftManifestationBridge> CraftManifestationBridges { get; set; }

        public ICollection<CreationGroupDesignManifestationBridge> DesignManifestationBridges { get; set; }

        public ICollection<CreationGroupServiceLineBridge> ServiceLineBridges { get; set; }
    }
}
