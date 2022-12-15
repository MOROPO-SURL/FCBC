namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtistDesignManifestationBridge
    {
        public System.Guid ArtistId { get; set; }

        public Artist Artist { get; set; }

        public System.Guid ManifestationId { get; set; }

        public DesignManifestation Manifestation { get; set; }
    }
}
