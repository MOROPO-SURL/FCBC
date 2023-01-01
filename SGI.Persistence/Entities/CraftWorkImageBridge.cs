namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CraftWorkImageBridge
    {
        public System.Guid WorkId { get; set; }

        public CraftWork Work { get; set; }

        public System.Guid ImageId { get; set; }

        public SAPRESSC.Middleware.Media.Persistence.Entities.Image Image { get; set; }
    }
}
