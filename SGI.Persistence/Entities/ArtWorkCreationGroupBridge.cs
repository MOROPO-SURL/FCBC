namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtWorkCreationGroupBridge
    {
        public System.Guid WorkId { get; set; }

        public ArtWork Work { get; set; }

        public System.Guid CreationGroupId { get; set; }

        public CreationGroup CreationGroup { get; set; }
    }
}
