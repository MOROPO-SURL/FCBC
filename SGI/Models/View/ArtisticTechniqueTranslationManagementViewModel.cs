namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtisticTechniqueTranslationManagementViewModel
    {
        public ArtisticTechniqueDisplayViewModel ArtisticTechnique { get; set; }

        public ArtisticTechniqueTranslationDisplayViewModel[] Translations { get; set; }

        public Persistence.Enumerations.TranslatableLanguage[] NotSelectedLanguages { get; set; }
    }
}
