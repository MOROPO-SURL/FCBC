using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class InstitutionInformationInputViewModel
    {
        [Required]
        [Display(Name = "SocialObject")]
        public string SocialObject { get; set; }

        [Required]
        [Display(Name = "Mission")]
        public string Mission { get; set; }

        [Required]
        [Display(Name = "Vision")]
        public string Vision { get; set; }

        [Required]
        [Display(Name = "Histoy")]
        public string Histoy { get; set; }
    }
}
