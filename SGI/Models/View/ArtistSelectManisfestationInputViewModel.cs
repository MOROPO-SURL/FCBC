using MOROPO.FCBC.SGI.Persistence.Entities;
using System;
using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistSelectManisfestationInputViewModel
    {        
        public System.Guid ArtistId { get; set; }

        public Dictionary<ArtisticManifestationGroup, SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]> SelectGroups { get; set; }

        public ArtistSelectManisfestationInputViewModel()
        {            
            SelectGroups = new Dictionary<ArtisticManifestationGroup, SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<Guid>[]>();
        }
    }
}
