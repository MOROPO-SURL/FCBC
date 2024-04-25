namespace MOROPO.FCBC.Galleries.Models.View
{
    public class ExhibitViewModel
    {
        public ExhibitViewModel()
        {
            Works = new System.Collections.Generic.List<Contract.AbstractWorkViewModel>();
        }

        public System.Guid Id { get; set; }

        public int Index { get; set; }

        public System.Collections.Generic.List<Contract.AbstractWorkViewModel> Works { get; set; }
    }
}
