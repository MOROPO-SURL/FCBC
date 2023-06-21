namespace MOROPO.FCBC.SGI.Models.View
{
    public class GroupCraftManisfestationsInputViewModel
    {
        public CreationGroupDisplayViewModel Group { get; set; }

        public SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] Manifestations { get; set; }
    }
}
