namespace MOROPO.FCBC.SGI.Models.View
{
    public class CrafWorkElaborationInputViewModel
    {
        public CraftWorkDisplayViewModel Work { get; set; }

        public SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] TechniqueSelectors { get; set; }

        public SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] MaterialSelectors { get; set; }
    }
}
