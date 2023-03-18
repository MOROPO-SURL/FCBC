using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistArtisticManisfestationsInputViewModel
    {        
        public ArtistDisplayViewModel Artist { get; set; }

        public SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] UngroupManifestations { get; set; }

        public Dictionary<Persistence.Entities.ArtisticManifestationGroup, SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]> SelectGroups { get; set; }

        public ArtistArtisticManisfestationsInputViewModel()
        {            
            SelectGroups = new Dictionary<Persistence.Entities.ArtisticManifestationGroup, SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]>();
        }
    }
}
