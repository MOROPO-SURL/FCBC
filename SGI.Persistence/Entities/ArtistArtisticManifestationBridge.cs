namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtistArtisticManifestationBridge
    {
        public System.Guid ArtistId { get; set; }

        public Artist Artist { get; set; }

        public System.Guid ManifestationId { get; set; }

        public ArtisticManifestation Manifestation { get; set; }
    }
}
