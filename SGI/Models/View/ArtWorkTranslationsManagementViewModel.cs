using MOROPO.FCBC.SGI.Persistence.Enumerations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtWorkTranslationsManagementViewModel
    {
        public ArtWorkDisplayViewModel Work { get; set; }

        public System.Collections.Generic.ICollection<ArtWorkTranslationDisplayViewModel> Translations { get; set; }

        public TranslatableLanguage[] NotSelectedLanguage { get; set; }
    }
}
