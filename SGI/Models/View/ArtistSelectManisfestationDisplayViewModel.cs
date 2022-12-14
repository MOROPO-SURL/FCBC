using MOROPO.FBCB.SGI.Persistence.Entities;
using System;
using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistSelectManisfestationDisplayViewModel
    {
        public ArtistDisplayViewModel Artist { get; set; }

        public Dictionary<ManifestationKind, SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]> Groups { get; set; }

        public ArtistSelectManisfestationDisplayViewModel()
        {
            Artist = new ArtistDisplayViewModel();
            Groups = new Dictionary<ManifestationKind, SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<Guid>[]>();
        }
    }
}
