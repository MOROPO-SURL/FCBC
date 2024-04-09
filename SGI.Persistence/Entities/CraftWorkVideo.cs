using MOROPO.FCBC.SGI.Persistence.Entities;
using System.Diagnostics.Contracts;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CraftWorkVideo : Contract.AbstractAttachment, SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("WorkId")]
        public CraftWork Work { get; set; }
        public System.Guid WorkId { get; set; }
    }
}
