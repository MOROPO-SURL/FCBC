using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CraftWorkMaterialBridge
    {
        public System.Guid WorkId { get; set; }

        public CraftWork Work { get; set; }

        public System.Guid MaterialId { get; set; }

        public Material Material { get; set; }
    }
}
