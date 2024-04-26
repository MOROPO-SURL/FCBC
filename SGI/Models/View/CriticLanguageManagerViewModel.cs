using MOROPO.FCBC.SGI.Persistence.Enumerations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CriticLanguageManagerViewModel
    {
        public CriticDisplayViewModel Critic { get; set; }

        public System.Collections.Generic.ICollection<CriticTranslationDisplayViewModel> Translations { get; set; }
        public TranslatableLanguage[] NotSelectedLanguage { get; set; }

    }
}
