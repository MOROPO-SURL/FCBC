using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CreationGroupAwardBridge
    {
        public System.Guid GroupId { get; set; }

        public CreationGroup Group { get; set; }

        public System.Guid AwardId { get; set; }

        public Award Award { get; set; }
    }
}
