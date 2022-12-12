using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class InstitutionInformationDisplayViewModel
    {
        [Display(Name = "SocialObject")]
        public string SocialObject { get; set; }

        [Display(Name = "Mission")]
        public string Mission { get; set; }

        [Display(Name = "Vision")]
        public string Vision { get; set; }

        [Display(Name = "Histoy")]
        public string Histoy { get; set; }
    }
}
