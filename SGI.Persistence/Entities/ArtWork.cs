using MOROPO.FCBC.SGI.Persistence.Entities.Contract;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtWork : AbstractWork
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ManifestationId")]
        public ArtisticManifestation Manifestation { get; set; }
        public System.Guid ManifestationId { get; set; }
    }
}
