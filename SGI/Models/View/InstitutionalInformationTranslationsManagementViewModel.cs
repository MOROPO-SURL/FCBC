namespace MOROPO.FCBC.SGI.Models.View
{
    public class InstitutionalInformationTranslationsManagementViewModel
    {
        public InstitutionalInformationDisplayViewModel InstitutionalInformation { get; set; }

        public InstitutionalInformationTranslationDisplayViewModel[] Translations { get; set; }

        public Persistence.Enumerations.TranslatableLanguage[] NotSelectedLanguages { get; set; }
    }
}
