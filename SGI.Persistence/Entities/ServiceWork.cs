namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ServiceWork : Contract.AbstractWork
    {
        public ServiceWork()
        {
            TechniqueBridges = new System.Collections.Generic.List<ServiceWorkTechniqueBridge>();
            ImageBridges = new System.Collections.Generic.List<ServiceWorkImageBridge>();
            Awards = new System.Collections.Generic.List<ServiceWorkAward>();
            MaterialBridges = new System.Collections.Generic.List<ServiceWorkMaterialBridge>();
            ThematicBridges = new System.Collections.Generic.List<ServiceWorkThematicBridge>();
            Attachments = new System.Collections.Generic.List<ServiceWorkAttachment>();
        }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("LineId")]
        public ServiceLine Line { get; set; }
        public System.Guid LineId { get; set; }

        public System.Collections.Generic.List<ServiceWorkTechniqueBridge> TechniqueBridges { get; set; }

        public System.Collections.Generic.List<ServiceWorkImageBridge> ImageBridges { get; set; }
        public System.Collections.Generic.List<ServiceWorkAward> Awards { get; set; }

        public System.Collections.Generic.List<ServiceWorkCreatorBridge> CreatorBridges { get; set; }

        public System.Collections.Generic.List<ServiceWorkCreationGroupBridge> CreationGroupBridges { get; set; }
        public System.Collections.Generic.List<ServiceWorkMaterialBridge> MaterialBridges { get; set; }
        public System.Collections.Generic.List<ServiceWorkThematicBridge> ThematicBridges { get; set; }

        public System.Collections.Generic.List<ServiceWorkAttachment> Attachments { get; set; }
    }
}
