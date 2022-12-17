namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtWork : Contract.AbstractWork
    {
        public ArtWork()
        {
            TechniqueBridges = new System.Collections.Generic.List<ArtWorkTechniqueBridge>();
            ImageBridges = new System.Collections.Generic.List<ArtWorkImageBridge>();
        }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ManifestationId")]
        public ArtisticManifestation Manifestation { get; set; }
        public System.Guid ManifestationId { get; set; }

        public System.Collections.Generic.List<ArtWorkTechniqueBridge> TechniqueBridges { get; set; }

        public System.Collections.Generic.List<ArtWorkImageBridge> ImageBridges { get; set; }
    }
}
