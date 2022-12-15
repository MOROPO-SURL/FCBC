using MOROPO.FCBC.SGI.Persistence.Entities.Contract;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ServiceWork : AbstractWork
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("LineId")]
        public ServiceLine Line { get; set; }
        public System.Guid LineId { get; set; }
    }
}
