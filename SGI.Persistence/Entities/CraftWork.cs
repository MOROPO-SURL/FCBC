namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CraftWork : Contract.AbstractWork
    {
        public CraftWork()
        {
            TechniqueBridges = new System.Collections.Generic.List<CraftWorkTechniqueBridge>();
            ImageBridges = new System.Collections.Generic.List<CraftWorkImageBridge>();
            Awards = new System.Collections.Generic.List<CraftWorkAward>();
        }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ManifestationId")]
        public CraftManifestation Manifestation { get; set; }
        public System.Guid ManifestationId { get; set; }

        public System.Collections.Generic.List<CraftWorkTechniqueBridge> TechniqueBridges { get; set; }

        public System.Collections.Generic.List<CraftWorkImageBridge> ImageBridges { get; set; }

        public System.Collections.Generic.List<CraftWorkAward> Awards { get; set; }
    }
}
