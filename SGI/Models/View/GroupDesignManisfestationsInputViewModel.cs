using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class GroupDesignManisfestationsInputViewModel
    {
        public CreationGroupDisplayViewModel Group { get; set; }

        public SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] UngroupManifestations { get; set; }

        public Dictionary<Persistence.Entities.DesignManifestationGroup, SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]> SelectGroups { get; set; }

        public GroupDesignManisfestationsInputViewModel()
        {
            SelectGroups = new Dictionary<Persistence.Entities.DesignManifestationGroup, SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]>();
        }
    }
}
