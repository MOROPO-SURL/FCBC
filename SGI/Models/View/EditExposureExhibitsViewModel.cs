namespace MOROPO.FCBC.SGI.Models.View
{
    public class EditExposureExhibitsViewModel
    {
        public ExposureDisplayViewModel Exposure { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableIdexes { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableArtWorks { get; set; }

        public ExhibitDisplayViewModel[] Exhibits { get; set; }
    }
}
