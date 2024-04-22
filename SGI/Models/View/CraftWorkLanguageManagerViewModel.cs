using MOROPO.FCBC.SGI.Persistence.Enumerations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CraftWorkLanguageManagerViewModel
    {
        public CraftWorkDisplayViewModel Work { get; set; }

        public System.Collections.Generic.ICollection<CraftWorkTranslationDisplayViewModel> Translations { get; set; }

        public TranslatableLanguage[] NotSelectedLanguage { get; set; }
    }
}
