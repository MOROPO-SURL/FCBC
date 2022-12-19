using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistCraftManisfestationsInputViewModel
    {
        public ArtistDisplayViewModel Artist { get; set; }

        public SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] UngroupManifestations { get; set; }

        public Dictionary<Persistence.Entities.CraftManifestationGroup, SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]> SelectGroups { get; set; }

        public ArtistCraftManisfestationsInputViewModel()
        {
            SelectGroups = new Dictionary<Persistence.Entities.CraftManifestationGroup, SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]>();
        }
    }
}
