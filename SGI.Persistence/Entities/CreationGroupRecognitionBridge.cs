using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CreationGroupRecognitionBridge
    {
        public System.Guid GroupId { get; set; }

        public CreationGroup Group { get; set; }

        public System.Guid RecognitionId { get; set; }

        public Recognition Recognition { get; set; }
    }
}
