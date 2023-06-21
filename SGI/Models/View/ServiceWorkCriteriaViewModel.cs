namespace MOROPO.FCBC.SGI.Models.View
{
    public class ServiceWorkCriteriaViewModel
    {
        public ServiceWorkDisplayViewModel Work { get; set; }

        public CriterialDisplayViewModel[] Criterials { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] CriticSelectListItems { get; set; }
    }
}
