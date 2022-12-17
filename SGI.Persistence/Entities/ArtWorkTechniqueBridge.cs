namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtWorkTechniqueBridge
    {
        public System.Guid WorkId { get; set; }

        public ArtWork Work { get; set; }

        public System.Guid TechniqueId { get; set; }

        public ArtisticTechnique Technique { get; set; }
    }
}
