namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtWorkElaborationInputViewModel
    {
        public ArtWorkDisplayViewModel Work { get; set; }

        public SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] TechniqueSelectors { get; set; }

        public SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] MaterialSelectors { get; set; }
    }
}
