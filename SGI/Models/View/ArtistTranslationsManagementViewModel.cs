namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistTranslationsManagementViewModel
    {
        public ArtistDisplayViewModel Artist { get; set; }

        public ArtistTranslationDisplayViewModel[] Translations { get; set; }

        public Persistence.Enumerations.TranslatableLanguage[] NotSelectedLanguages { get; set; }
    }
}
