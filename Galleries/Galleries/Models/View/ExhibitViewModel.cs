namespace MOROPO.FCBC.Galleries.Models.View
{
    public class ExhibitViewModel
    {
        public ExhibitViewModel()
        {
            ArtWorks = new System.Collections.Generic.List<ArtWorkViewModel>();
        }

        public System.Guid Id { get; set; }

        public int Index { get; set; }

        public System.Collections.Generic.List<ArtWorkViewModel> ArtWorks { get; set; }
    }
}
