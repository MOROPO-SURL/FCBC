using MOROPO.FCBC.SGI.Persistence.Enumerations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class MaterialLanguageManagerViewModel
    {
        public MaterialDisplayViewModel Material { get; set; }

        public System.Collections.Generic.ICollection<MaterialTranslationDisplayViewModel> Translations { get; set; }
        public TranslatableLanguage[] NotSelectedLanguage { get; set; }
    }
}
