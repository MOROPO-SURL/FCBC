namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtWorkTranslation
    {
        public Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        public System.Guid ArtWorkId { get; set; }
        public ArtWork ArtWork { get; set; }

        public string Title { get; set; }

        public string Collection { get; set; }

        public string Location { get; set; }

        public string About { get; set; }
    }
}
