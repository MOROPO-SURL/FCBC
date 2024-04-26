using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CriticLanguagesInputViewModel: LanguagesInputViewModel
    {
        [Display(Name = "About")]
        public string About { get; set; }
    }
}
