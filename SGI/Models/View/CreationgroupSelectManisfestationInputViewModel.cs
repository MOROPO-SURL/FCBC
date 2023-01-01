using MOROPO.FCBC.SGI.Persistence.Entities;
using System.Collections.Generic;
using System;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CreationgroupSelectManisfestationInputViewModel
    {
        public System.Guid GroupId { get; set; }

        public Dictionary<ArtisticManifestationGroup, SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]> SelectGroups { get; set; }

        public CreationgroupSelectManisfestationInputViewModel()
        {
            SelectGroups = new Dictionary<ArtisticManifestationGroup, SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<Guid>[]>();
        }
    }
}
