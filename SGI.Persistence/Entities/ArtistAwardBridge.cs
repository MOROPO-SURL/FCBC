using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtistAwardBridge
    {
        public System.Guid ArtistId { get; set; }

        public Artist Artist { get; set; }

        public System.Guid AwardId { get; set; }

        public Award Award { get; set; }
    }
}
