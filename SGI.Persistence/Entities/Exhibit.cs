using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class Exhibit : SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public Exhibit()
        {
            ArtWorkBridges = new List<ExhibitArtWorkBridge>();
            CraftWorkBridges = new List<ExhibitCraftWorkBridge>();
            DesignWorkBridges = new List<ExhibitDesignWorkBridge>();
            ServiceWorkBridges = new List<ExhibitServiceWorkBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        public int Index { get; set; }

        public Exposure Exposure { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? ExposureId { get; set; }

        public List<ExhibitArtWorkBridge> ArtWorkBridges { get; set; }
        
        public List<ExhibitCraftWorkBridge> CraftWorkBridges { get; set; }

        public List<ExhibitDesignWorkBridge> DesignWorkBridges { get; set; }

        public List<ExhibitServiceWorkBridge> ServiceWorkBridges { get; set; }
    }
}
