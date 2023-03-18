namespace MOROPO.FCBC.SGI.Models.View
{
    public class CraftWorkThematicInputViewModel
    {
        public CraftWorkDisplayViewModel Work { get; set; }

        public SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] ThematicSelectors { get; set; } = new SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] { };
    }
}
