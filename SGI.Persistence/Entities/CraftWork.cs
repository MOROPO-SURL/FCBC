namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CraftWork : Contract.AbstractWork
    {
        public CraftWork()
        {
            TechniqueBridges = new System.Collections.Generic.List<CraftWorkTechniqueBridge>();
            ImageBridges = new System.Collections.Generic.List<CraftWorkImageBridge>();
            Awards = new System.Collections.Generic.List<CraftWorkAward>();
            CreatorBridges = new System.Collections.Generic.List<CraftWorkCreatorBridge>();
            ThematicBridges = new System.Collections.Generic.List<CraftWorkThematicBridge>();
            CreationGroupBridges = new System.Collections.Generic.List<CraftWorkCreationGroupBridge>();
            MaterialBridges = new System.Collections.Generic.List<CraftWorkMaterialBridge>();
            Attachments = new System.Collections.Generic.List<CraftWorkAttachment>();
        }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ManifestationId")]
        public CraftManifestation Manifestation { get; set; }
        public System.Guid ManifestationId { get; set; }

        public System.Collections.Generic.List<CraftWorkTechniqueBridge> TechniqueBridges { get; set; }

        public System.Collections.Generic.List<CraftWorkImageBridge> ImageBridges { get; set; }

        public System.Collections.Generic.List<CraftWorkAward> Awards { get; set; }

        public System.Collections.Generic.List<CraftWorkCreatorBridge> CreatorBridges { get; set; }
        public System.Collections.Generic.List<CraftWorkThematicBridge> ThematicBridges { get; set; }

        public System.Collections.Generic.List<CraftWorkCreationGroupBridge> CreationGroupBridges { get; set; }
        public System.Collections.Generic.List<CraftWorkMaterialBridge> MaterialBridges { get; set; }
        public System.Collections.Generic.List<CraftWorkAttachment> Attachments { get; set; }
    }
}
