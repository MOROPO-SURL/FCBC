namespace MOROPO.FCBC.SGI.Models.View
{
    public class GalleryTranslationsManagementViewModel
    {
        public GalleryDisplayViewModel Gallery { get; set; }

        public GalleryTranslationDisplayViewModel[] Translations { get; set; }

        public Persistence.Enumerations.TranslatableLanguage[] NotSelectedLanguages { get; set; }
    }
}
