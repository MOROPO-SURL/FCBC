using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class GrouptServiceLinesInputViewModel
    {
        public CreationGroupDisplayViewModel Group { get; set; }

        public SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] UngroupManifestations { get; set; }

        public Dictionary<Persistence.Entities.ServiceLineGroup, SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]> SelectGroups { get; set; }

        public GrouptServiceLinesInputViewModel()
        {
            SelectGroups = new Dictionary<Persistence.Entities.ServiceLineGroup, SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]>();
        }
    }
}
