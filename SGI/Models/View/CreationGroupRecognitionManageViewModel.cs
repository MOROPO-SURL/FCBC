namespace MOROPO.FCBC.SGI.Models.View
{
    public class CreationGroupRecognitionManageViewModel
    {
        public CreationGroupRecognitionManageViewModel()
        {
            RecognitionsToSelect = new System.Collections.Generic.List<SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>>();
        }

        public CreationGroupDisplayViewModel Group { get; set; }

        public System.Collections.Generic.List<SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>> RecognitionsToSelect { get; set; }
    }
}
