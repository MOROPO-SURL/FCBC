namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ServiceWork : Contract.AbstractWork
    {
        public ServiceWork()
        {
            TechniqueBridges = new System.Collections.Generic.List<ServiceWorkTechniqueBridge>();
            ImageBridges = new System.Collections.Generic.List<ServiceWorkImageBridge>();
        }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("LineId")]
        public ServiceLine Line { get; set; }
        public System.Guid LineId { get; set; }

        public System.Collections.Generic.List<ServiceWorkTechniqueBridge> TechniqueBridges { get; set; }

        public System.Collections.Generic.List<ServiceWorkImageBridge> ImageBridges { get; set; }
    }
}
