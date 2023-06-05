namespace MOROPO.FCBC.SGI.Models.View
{
    public class GalleryEmployeesManageViewModel
    {
        public EmployeeDisplayViewModel[] Members { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] NotMemberSelectListItems { get; set; }
    }
}
