using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ServiceWorkAttachment: Contract.AbstractAttachment, SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("WorkId")]
        public ServiceWork Work { get; set; }
        public System.Guid WorkId { get; set; }
    }
}
