
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class ArtisticManifestation : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ParentId")]
        public ManifestationKind Parent { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid ParentId { get; set; }

        public ICollection<ArtistManifestationBridge> ArtistManifestationBridges { get; set; }

        public ArtisticManifestation()
        {
            Id = System.Guid.NewGuid();
            ArtistManifestationBridges = new List<ArtistManifestationBridge>();
        }
        
    }
}
