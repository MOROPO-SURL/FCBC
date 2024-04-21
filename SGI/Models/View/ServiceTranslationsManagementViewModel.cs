using MOROPO.FCBC.SGI.Persistence.Enumerations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ServiceTranslationsManagementViewModel
    {
        public ServiceDisplayViewModel Service { get; set; }

        public System.Collections.Generic.ICollection<InstitutionalServiceTranslationDisplayViewModel> Translations { get; set; }

        public TranslatableLanguage[] NotSelectedLanguage { get; set; }
    }
}
