using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CraftWorkCreationGroupBridge
    {
        public System.Guid WorkId { get; set; }

        public CraftWork Work { get; set; }

        public System.Guid CreationGroupId { get; set; }

        public CreationGroup CreationGroup { get; set; }
    }
}
