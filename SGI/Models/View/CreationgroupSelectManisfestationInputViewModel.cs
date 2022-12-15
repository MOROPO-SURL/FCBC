using MOROPO.FBCB.SGI.Persistence.Entities;
using System.Collections.Generic;
using System;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CreationgroupSelectManisfestationInputViewModel
    {
        public System.Guid GroupId { get; set; }

        public Dictionary<ManifestationKind, SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]> SelectGroups { get; set; }

        public CreationgroupSelectManisfestationInputViewModel()
        {
            SelectGroups = new Dictionary<ManifestationKind, SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<Guid>[]>();
        }
    }
}
