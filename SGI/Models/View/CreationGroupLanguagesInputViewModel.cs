using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CreationGroupLanguagesInputViewModel: LanguagesInputViewModel
    {
        [Display(Name ="Name")]
        public string Name { get; set; }
        [Display(Name = "Brand")]
        public string Brand { get; set; }
        [Display(Name = "About")]
        public string About { get; set; }
    }
}
