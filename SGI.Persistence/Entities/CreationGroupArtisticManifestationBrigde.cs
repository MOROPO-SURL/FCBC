using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class CreationGroupArtisticManifestationBrigde
    {
        public System.Guid CreationGroupId { get; set; }

        public CreationGroup CreationGroup { get; set; }

        public System.Guid ArtisticManifestationId { get; set; }

        public ArtisticManifestation ArtisticManifestation { get; set; }
    }
}
