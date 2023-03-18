namespace MOROPO.FCBC.SGI.Models.View
{
    public class DesignWorkElaborationInputViewModel
    {
        public DesignWorkDisplayViewModel Work { get; set; }

        public SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] TechniqueSelectors { get; set; } = new SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] { };

        public SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] MaterialSelectors { get; set; } = new SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] { };
    }
}
