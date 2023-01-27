namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtWork : Contract.AbstractWork
    {
        public ArtWork()
        {
            TechniqueBridges = new System.Collections.Generic.List<ArtWorkTechniqueBridge>();
            ImageBridges = new System.Collections.Generic.List<ArtWorkImageBridge>();
            MaterialBridges = new System.Collections.Generic.List<ArtWorkMaterialBridge>();
            CreatorBridges = new System.Collections.Generic.List<ArtWorkCreatorBridge>();
            CreationGroupBridges = new System.Collections.Generic.List<ArtWorkCreationGroupBridge>();
            ArtWorkThematicBridges = new System.Collections.Generic.List<ArtWorkThematicBridge>();
            Valorations = new System.Collections.Generic.List<ArtWorkValoration>();
            Awards = new System.Collections.Generic.List<ArtWorkAward>();
            Attachments = new System.Collections.Generic.List<ArtWorkAttachment>();            
        }

        public string Collection { get; set; }

        public string Location { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ManifestationId")]
        public ArtisticManifestation Manifestation { get; set; }
        public System.Guid ManifestationId { get; set; }

        public decimal? Profundity { get; set; }

        public decimal? Height { get; set; }

        public decimal? Width { get; set; }

        public UnitMeasurement DimensionsUnitMeasurement { get; set; }
        public System.Guid? DimensionsUnitMeasurementId { get; set; }

        public decimal? Weight { get; set; }

        public UnitMeasurement WeightUnitMeasurement { get; set; }
        public System.Guid? WeightUnitMeasurementId { get; set; }

        public System.Collections.Generic.List<ArtWorkTechniqueBridge> TechniqueBridges { get; set; }

        public System.Collections.Generic.List<ArtWorkMaterialBridge> MaterialBridges { get; set; }

        public System.Collections.Generic.List<ArtWorkImageBridge> ImageBridges { get; set; }

        public System.Collections.Generic.List<ArtWorkCreatorBridge> CreatorBridges { get; set; }
        public System.Collections.Generic.List<ArtWorkThematicBridge> ArtWorkThematicBridges { get; set; }

        public System.Collections.Generic.List<ArtWorkCreationGroupBridge> CreationGroupBridges { get; set; }

        public System.Collections.Generic.List<ArtWorkValoration> Valorations { get; set; }

        public System.Collections.Generic.List<ArtWorkAward> Awards { get; set; }

        public System.Collections.Generic.List<ArtWorkAttachment> Attachments { get; set; }        
    }
}
