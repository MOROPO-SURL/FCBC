namespace MOROPO.FCBC.SGI.Models.View
{
    public class GrouptServiceLinesInputViewModel
    {
        public CreationGroupDisplayViewModel Group { get; set; }

        public SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] Manifestations { get; set; }
    }
}
