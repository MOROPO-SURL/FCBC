using MOROPO.FCBC.SGI.Persistence.Enumerations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ServiceWorkLanguageManagerViewModel
    {
        public ServiceWorkDisplayViewModel Work { get; set; }

        public System.Collections.Generic.ICollection<ServiceWorkTranslationDisplayViewModel> Translations { get; set; }

        public TranslatableLanguage[] NotSelectedLanguage { get; set; }
    }
}
