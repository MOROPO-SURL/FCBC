namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtWorkTranslationDisplayViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        public string Title { get; set; }

        public string Collection { get; set; }

        public string Location { get; set; }

        public string About { get; set; }
    }
}
