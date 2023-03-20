namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtWorkValorationsViewModel
    {
        public ArtWorkDisplayViewModel Work { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] SelectCritics { get; set; }

        public ValorationDisplayViewModel[] Valorations { get; set; }
    }
}
