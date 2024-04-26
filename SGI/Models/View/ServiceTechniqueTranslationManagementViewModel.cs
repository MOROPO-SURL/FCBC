namespace MOROPO.FCBC.SGI.Models.View
{
    public class ServiceTechniqueTranslationManagementViewModel
    {
        public ServiceTechniqueDisplayViewModel ServiceTechnique { get; set; }

        public ServiceTechniqueTranslationDisplayViewModel[] Translations { get; set; }

        public Persistence.Enumerations.TranslatableLanguage[] NotSelectedLanguages { get; set; }
    }
}
