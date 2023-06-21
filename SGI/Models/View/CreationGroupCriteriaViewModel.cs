namespace MOROPO.FCBC.SGI.Models.View
{
    public class CreationGroupCriteriaViewModel
    {
        public CreationGroupDisplayViewModel CreationGroup { get; set; }

        public CriterialDisplayViewModel[] Criterials { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] CriticSelectListItems { get; set; }
    }
}
