using MOROPO.FCBC.SGI.Persistence.Enumerations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class GalleryTranslationsManagementViewModel
    {
        public GalleryDisplayViewModel Gallery { get; set; }

        public GalleryTranslationDisplayViewModel[] Translations { get; set; }

        public TranslatableLanguage[] NotSelectedLanguages { get; set; }
    }
}
