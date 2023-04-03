using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtWorkAwardManageViewModel
    {
        public ArtWorkDisplayViewModel ArtWork { get; set; }

        public ICollection<SelectListItem> SelectAwards { get; set; } = new List<SelectListItem>();        
    }
}
