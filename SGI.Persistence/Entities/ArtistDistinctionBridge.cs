using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtistDistinctionBridge
    {
        public System.Guid ArtistId { get; set; }

        public Artist Artist { get; set; }
        
        public System.Guid DistinctionId { get; set; }

        public Distinction Distinction { get; set; }
    }
}
