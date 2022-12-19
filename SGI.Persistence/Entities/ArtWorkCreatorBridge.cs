namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtWorkCreatorBridge
    {
        public System.Guid WorkId { get; set; }

        public ArtWork Work { get; set; }

        public System.Guid CreatorId { get; set; }

        public Artist Creator { get; set; }
    }
}
