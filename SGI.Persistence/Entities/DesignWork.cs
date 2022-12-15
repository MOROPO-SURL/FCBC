namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class DesignWork : Contract.AbstractWork
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ManifestationId")]
        public DesignManifestation Manifestation { get; set; }
        public System.Guid ManifestationId { get; set; }
    }
}
