using MOROPO.FCBC.SGI.Persistence.Enumerations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ThematicLanguageManagerViewModel
    {
        public ThematicDisplayViewModel Thematic { get; set; }

        public System.Collections.Generic.ICollection<ThematicTranslationDisplayViewModel> Translations { get; set; }
        public TranslatableLanguage[] NotSelectedLanguage { get; set; }
    }
}
