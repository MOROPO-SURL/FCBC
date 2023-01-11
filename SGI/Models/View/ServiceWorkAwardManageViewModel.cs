using Microsoft.AspNetCore.Mvc.Rendering;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ServiceWorkAwardManageViewModel
    {
        public ServiceWorkDisplayViewModel ServiceWorkDisplay { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Award")]
        public System.Guid AwardId { get; set; }

        public System.Collections.Generic.ICollection<SelectListItem> SelectAwards { get; set; } = new System.Collections.Generic.List<SelectListItem>();

        [System.ComponentModel.DataAnnotations.Display(Name = "About")]
        public string About { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Year")]
        public int Year { get; set; }
    }
}
