using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class GroupCraftManisfestationsInputViewModel
    {
        public CreationGroupDisplayViewModel Group { get; set; }

        public SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] UngroupManifestations { get; set; }

        public Dictionary<Persistence.Entities.CraftManifestationGroup, SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]> SelectGroups { get; set; }

        public GroupCraftManisfestationsInputViewModel()
        {
            SelectGroups = new Dictionary<Persistence.Entities.CraftManifestationGroup, SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]>();
        }
    }
}
