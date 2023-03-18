namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CreationGroupArtisticManifestationBridge
    {
        public System.Guid GroupId { get; set; }

        public CreationGroup Group { get; set; }

        public System.Guid ManifestationId { get; set; }

        public ArtisticManifestation Manifestation { get; set; }
    }
}
