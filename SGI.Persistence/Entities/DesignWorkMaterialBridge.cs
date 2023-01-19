using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class DesignWorkMaterialBridge
    {
        public System.Guid WorkId { get; set; }

        public DesignWork Work { get; set; }

        public System.Guid MaterialId { get; set; }

        public Material Material { get; set; }
    }
}
