namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtWorkCriteriaViewModel
    {
        public ArtWorkDisplayViewModel Work { get; set; }

        public CriterialDisplayViewModel[] Criterials { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] CriticSelectListItems { get; set; }
    }
}
