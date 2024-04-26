namespace MOROPO.FCBC.SGI.Models.View
{
    public class EditExposureExhibitsViewModel
    {
        public ExposureDisplayViewModel Exposure { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableIndexes { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableArtWorks { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableCraftWorks { get; set; }

        public ExhibitDisplayViewModel[] Exhibits { get; set; }
    }
}
