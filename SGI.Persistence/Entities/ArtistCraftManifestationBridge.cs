namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtistCraftManifestationBridge
    {
        public System.Guid ArtistId { get; set; }

        public Artist Artist { get; set; }

        public System.Guid ManifestationId { get; set; }

        public CraftManifestation Manifestation { get; set; }
    }
}
