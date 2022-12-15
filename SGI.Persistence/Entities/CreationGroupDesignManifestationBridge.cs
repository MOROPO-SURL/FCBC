namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CreationGroupDesignManifestationBridge
    {
        public System.Guid GroupId { get; set; }

        public CreationGroup Group { get; set; }

        public System.Guid ManifestationId { get; set; }

        public DesignManifestation Manifestation { get; set; }
    }
}
