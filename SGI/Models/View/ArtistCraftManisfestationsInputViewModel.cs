namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistCraftManisfestationsInputViewModel
    {
        public ArtistDisplayViewModel Artist { get; set; }

        public SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[] Manifestations { get; set; }
    }
}
