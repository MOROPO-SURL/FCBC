namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ServiceWorkTechniqueBridge
    {
        public System.Guid WorkId { get; set; }

        public ServiceWork Work { get; set; }

        public System.Guid TechniqueId { get; set; }

        public ServiceTechnique Technique { get; set; }
    }
}
