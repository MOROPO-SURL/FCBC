namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ExhibitServiceWorkBridge
    {
        public System.Guid ExhibitId { get; set; }
        public Exhibit Exhibit { get; set; }

        public System.Guid ServiceWorkId { get; set; }
        public ServiceWork ServiceWork { get; set; }
    }
}
