using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtistRecognitionBridge
    {
        public System.Guid RecognitionId { get; set; }

        public Recognition Recognition { get; set; }

        public System.Guid ArtistId { get; set; }

        public Artist Artist { get; set; }
    }
}
