using MOROPO.FCBC.SGI.Persistence.Enumerations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CreationGroupLanguageManagerViewModel
    {
        public CreationGroupDisplayViewModel CreationGroup { get; set; }

        public System.Collections.Generic.ICollection<CreationGroupTranslationDisplayViewModel> Translations { get; set; }

        public TranslatableLanguage[] NotSelectedLanguage { get; set; }
    }
}
