namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtWorkMaterialBridge
    {
        public System.Guid WorkId { get; set; }

        public ArtWork Work { get; set; }

        public System.Guid MaterialId { get; set; }

        public Material Material { get; set; }
    }
}
