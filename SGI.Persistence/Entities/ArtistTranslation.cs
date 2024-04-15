namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtistTranslation
    {
        public Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        public System.Guid ArtistId { get; set; }
        public Artist Artist { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Description { get; set; }
    }
}
