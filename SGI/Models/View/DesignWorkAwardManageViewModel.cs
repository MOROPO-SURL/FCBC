using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class DesignWorkAwardManageViewModel
    {
        public DesignWorkDisplayViewModel DesignWorkDisplay { get; set; }

        [Display(Name = "Award")]
        public System.Guid AwardId { get; set; }

        public ICollection<SelectListItem> SelectAwards { get; set; } = new List<SelectListItem>();

        [Display(Name = "Context")]
        public string Context { get; set; }

        [Display(Name = "Year")]
        public int Year { get; set; }
    }
}
