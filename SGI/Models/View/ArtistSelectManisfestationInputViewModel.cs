using MOROPO.FBCB.SGI.Persistence.Entities;
using System;
using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistSelectManisfestationInputViewModel
    {        
        public System.Guid ArtistId { get; set; }

        public Dictionary<ManifestationKind, SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]> SelectGroups { get; set; }

        public ArtistSelectManisfestationInputViewModel()
        {            
            SelectGroups = new Dictionary<ManifestationKind, SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<Guid>[]>();
        }
    }
}
