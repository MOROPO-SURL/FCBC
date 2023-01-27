using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class DesignWorkThematicBridge
    {
        public System.Guid WorkId { get; set; }

        public DesignWork Work { get; set; }

        public System.Guid ThematicId { get; set; }

        public Thematic Thematic { get; set; }
    }
}
