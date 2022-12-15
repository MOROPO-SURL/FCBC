using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class ArtistManifestationBridge
    {
        public System.Guid ArtistId { get; set; }

        public Artist Artist { get; set; }

        public System.Guid ArtisticManifestationId { get; set; }

        public ArtisticManifestation ArtisticManifestation { get; set; }
    }
}
