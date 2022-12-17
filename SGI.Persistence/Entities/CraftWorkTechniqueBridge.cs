namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CraftWorkTechniqueBridge
    {
        public System.Guid WorkId { get; set; }

        public CraftWork Work { get; set; }

        public System.Guid TechniqueId { get; set; }

        public CraftTechnique Technique { get; set; }
    }
}
