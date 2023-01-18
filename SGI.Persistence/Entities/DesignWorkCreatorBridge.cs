
namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class DesignWorkCreatorBridge
    {
        public System.Guid WorkId { get; set; }

        public DesignWork Work { get; set; }

        public System.Guid CreatorId { get; set; }

        public Artist Creator { get; set; }
    }
}
