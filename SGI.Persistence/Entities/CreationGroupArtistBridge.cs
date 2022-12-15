
namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CreationGroupArtistBridge
    {
        public System.Guid CreationGroupId { get; set; }

        public  CreationGroup CreationGroup { get; set; }

        public System.Guid ArtistId { get; set; }

        public Artist Artist { get; set; }
    }
}
