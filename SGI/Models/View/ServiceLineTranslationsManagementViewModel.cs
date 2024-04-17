namespace MOROPO.FCBC.SGI.Models.View
{
    public class ServiceLineTranslationsManagementViewModel
    {
        public ServiceLineDisplayViewModel ServiceLine { get; set; }

        public ServiceLineTranslationDisplayViewModel[] Translations { get; set; }

        public Persistence.Enumerations.TranslatableLanguage[] NotSelectedLanguages { get; set; }
    }
}
