using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ActivityLanguagesInputViewModel: LanguagesInputViewModel
    {
        [Display(Name = "Description")]
        public string Description { get; set; }        
    }
}
