using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CreationGroupDistinctionBridge
    {
        public System.Guid GroupId { get; set; }

        public CreationGroup Group { get; set; }

        public System.Guid DistinctionId { get; set; }

        public Distinction Distinction { get; set; }
    }
}
