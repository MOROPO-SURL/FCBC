using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CraftWorkThreeDimensional: Contract.AbstractAttachment, SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("WorkId")]
        public CraftWork Work { get; set; }
        public System.Guid WorkId { get; set; }
    }
}
