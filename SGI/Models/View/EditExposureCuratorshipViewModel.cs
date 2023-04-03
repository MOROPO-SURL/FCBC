namespace MOROPO.FCBC.SGI.Models.View
{
    public class EditExposureCuratorshipViewModel
    {
        public ExposureDisplayViewModel Exposure { get; set; }

        public EmployeeDisplayViewModel[] Employees { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableEmployeeSelectItems { get; set; }
    }
}
