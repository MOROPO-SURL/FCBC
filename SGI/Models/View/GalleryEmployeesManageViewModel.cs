using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class GalleryEmployeesManageViewModel
    {
        public GalleryDisplayViewModel Gallery { get; set; }
        public EmployeeDisplayViewModel[] Members { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] NotMemberSelectListItems { get; set; }
    }
}
