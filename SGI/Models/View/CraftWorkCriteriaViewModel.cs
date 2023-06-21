namespace MOROPO.FCBC.SGI.Models.View
{
    public class CraftWorkCriteriaViewModel
    {
        public CraftWorkDisplayViewModel Work { get; set; }

        public CriterialDisplayViewModel[] Criterials { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] CriticSelectListItems { get; set; }
    }
}
