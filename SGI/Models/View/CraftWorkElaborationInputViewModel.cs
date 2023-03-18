namespace MOROPO.FCBC.SGI.Models.View
{
    public class CraftWorkElaborationInputViewModel
    {
        public CraftWorkDisplayViewModel Work { get; set; }

        public SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] TechniqueSelectors { get; set; } = new SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]{};

        public SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] MaterialSelectors { get; set; } = new SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] { };
    }
}
