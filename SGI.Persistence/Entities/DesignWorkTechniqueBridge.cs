namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class DesignWorkTechniqueBridge
    {
        public System.Guid WorkId { get; set; }

        public DesignWork Work { get; set; }

        public System.Guid TechniqueId { get; set; }

        public DesignTechnique Technique { get; set; }
    }
}
