using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class GalleryTranslationDisplayViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        public string Name { get; set; }

        public string About { get; set; }

        public string History { get; set; }
    }
}
