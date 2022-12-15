using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistSelectManisfestationDisplayViewModel
    {
        public ArtistDisplayViewModel Artist { get; set; }

        public Dictionary<Persistence.Entities.ArtisticManifestationGroup, SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]> Groups { get; set; }

        public ArtistSelectManisfestationDisplayViewModel()
        {
            Artist = new ArtistDisplayViewModel();
            Groups = new Dictionary<Persistence.Entities.ArtisticManifestationGroup, SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]>();
        }
    }
}
