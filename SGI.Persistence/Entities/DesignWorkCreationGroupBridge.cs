
namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class DesignWorkCreationGroupBridge
    {
        public System.Guid WorkId { get; set; }

        public DesignWork Work { get; set; }

        public System.Guid CreationGroupId { get; set; }

        public CreationGroup CreationGroup { get; set; }
    }
}
