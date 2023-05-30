namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistCriterionViewModel
    {
        public ArtistDisplayViewModel Artist { get; set; }

        public CriterialDisplayViewModel[] Criterials { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] CriticSelectListItems { get; set; }
    }
}
