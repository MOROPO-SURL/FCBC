using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class InstitutionalInformationTranslationInputViewModel
    {
        [Display(Name = "Language")]
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        public Persistence.Enumerations.InstitutionalInformationKind InstitutionalInformationKindId { get; set; }
        public string SocialObject { get; set; }
        public string Mission { get; set; }
        public string Vision { get; set; }
        public string History { get; set; }
    }
}
