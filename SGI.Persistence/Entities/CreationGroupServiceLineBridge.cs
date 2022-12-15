namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CreationGroupServiceLineBridge
    {
        public System.Guid GroupId { get; set; }

        public CreationGroup Group { get; set; }

        public System.Guid LineId { get; set; }

        public ServiceLine Line { get; set; }
    }
}
