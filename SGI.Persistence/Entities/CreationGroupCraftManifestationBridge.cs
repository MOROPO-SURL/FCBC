namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CreationGroupCraftManifestationBridge
    {
        public System.Guid GroupId { get; set; }

        public CreationGroup Group { get; set; }

        public System.Guid ManifestationId { get; set; }

        public CraftManifestation Manifestation { get; set; }
    }
}
