using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ContractorAttachment : Contract.AbstractAttachment, SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ContractorId")]
        public Contractor Contractor { get; set; }
        public System.Guid ContractorId { get; set; }
    }
}
