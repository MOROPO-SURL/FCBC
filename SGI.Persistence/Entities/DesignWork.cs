namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class DesignWork : Contract.AbstractWork
    {
        public DesignWork()
        {
            TechniqueBridges = new System.Collections.Generic.List<DesignWorkTechniqueBridge>();
            ImageBridges = new System.Collections.Generic.List<DesignWorkImageBridge>();
        }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ManifestationId")]
        public DesignManifestation Manifestation { get; set; }
        public System.Guid ManifestationId { get; set; }

        public System.Collections.Generic.List<DesignWorkTechniqueBridge> TechniqueBridges { get; set; }

        public System.Collections.Generic.List<DesignWorkImageBridge> ImageBridges { get; set; }
    }
}
