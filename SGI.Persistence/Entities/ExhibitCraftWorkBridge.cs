namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ExhibitCraftWorkBridge
    {
        public System.Guid ExhibitId { get; set; }
        public Exhibit Exhibit { get; set; }

        public System.Guid CraftWorkId { get; set; }
        public CraftWork CraftWork { get; set; }
    }
}
