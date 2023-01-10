using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CraftWorkAwardManageViewModel
    {
        public CraftWorkDisplayViewModel CraftWorkDisplay { get; set; }

        [Display(Name = "Award")]
        public System.Guid AwardId { get; set; }

        public ICollection<SelectListItem> SelectAwards { get; set; } = new List<SelectListItem>();

        [Display(Name = "About")]
        public string About { get; set; }

        [Display(Name = "Year")]
        public int Year { get; set; }
    }
}
