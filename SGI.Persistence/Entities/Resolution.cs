using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class Resolution
    {
        public System.Guid Id { get; set; }
        public string NumberResolution { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
