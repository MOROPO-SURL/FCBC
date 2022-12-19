using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class GroupServiceLinesInputViewModel
    {
        public CreationGroupDisplayViewModel Group { get; set; }

        public SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] UngroupManifestations { get; set; }

        public Dictionary<Persistence.Entities.ServiceLineGroup, SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]> SelectGroups { get; set; }

        public GroupServiceLinesInputViewModel()
        {
            SelectGroups = new Dictionary<Persistence.Entities.ServiceLineGroup, SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]>();
        }
    }
}
