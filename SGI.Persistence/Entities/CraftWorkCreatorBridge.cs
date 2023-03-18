using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CraftWorkCreatorBridge
    {
        public System.Guid WorkId { get; set; }

        public CraftWork Work { get; set; }

        public System.Guid CreatorId { get; set; }

        public Artist Creator { get; set; }
    }
}
