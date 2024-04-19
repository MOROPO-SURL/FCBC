namespace MOROPO.FCBC.SGI.Models.View
{
    public class ExposureTranslationsManagementViewModel
    {
        public ExposureDisplayViewModel Exposure { get; set; }

        public ExposureTranslationDisplayViewModel[] Translations { get; set; }

        public Persistence.Enumerations.TranslatableLanguage[] NotSelectedLanguages { get; set; }
    }
}
