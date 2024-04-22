using MOROPO.FCBC.SGI.Persistence.Enumerations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ActivityLanguageManagerViewModel
    {
        public ActivityDisplayViewModel Activity { get; set; }

        public System.Collections.Generic.ICollection<ActivityTranslationDisplayViewModel> Translations { get; set; }

        public TranslatableLanguage[] NotSelectedLanguage { get; set; }
    }
}
