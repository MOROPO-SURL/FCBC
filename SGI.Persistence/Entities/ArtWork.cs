using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtWork : Contract.AbstractWork
    {
        public ArtWork()
        {
            TechniqueBridges = new List<ArtWorkTechniqueBridge>();
            ImageBridges = new List<ArtWorkImageBridge>();
            MaterialBridges = new List<ArtWorkMaterialBridge>();
            CreatorBridges = new List<ArtWorkCreatorBridge>();
            CreationGroupBridges = new List<ArtWorkCreationGroupBridge>();
            ThematicBridges = new List<ArtWorkThematicBridge>();
            Valorations = new List<ArtWorkValoration>();
            Awards = new List<ArtWorkAward>();
            Attachments = new List<ArtWorkAttachment>();
            Videos = new List<ArtWorkVideo>();
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

        public List<ArtWorkTechniqueBridge> TechniqueBridges { get; set; }

        public List<ArtWorkMaterialBridge> MaterialBridges { get; set; }

        public List<ArtWorkImageBridge> ImageBridges { get; set; }

        public List<ArtWorkCreatorBridge> CreatorBridges { get; set; }

        public List<ArtWorkThematicBridge> ThematicBridges { get; set; }

        public List<ArtWorkCreationGroupBridge> CreationGroupBridges { get; set; }

        public List<ArtWorkValoration> Valorations { get; set; }

        public List<ArtWorkAward> Awards { get; set; }

        public List<ArtWorkAttachment> Attachments { get; set; }       
        public List<ArtWorkVideo> Videos { get; set; }       
    }
}
