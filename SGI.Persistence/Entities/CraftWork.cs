using MOROPO.FCBC.SGI.Persistence.Entities.Contract;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CraftWork : AbstractWork
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ManifestationId")]
        public CraftManifestation Manifestation { get; set; }
        public System.Guid ManifestationId { get; set; }
    }
}
