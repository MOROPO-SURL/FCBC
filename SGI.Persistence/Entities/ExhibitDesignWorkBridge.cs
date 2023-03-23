namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ExhibitDesignWorkBridge
    {
        public System.Guid ExhibitId { get; set; }
        public Exhibit Exhibit { get; set; }

        public System.Guid DesignWorkId { get; set; }
        public DesignWork DesignWork { get; set; }
    }
}