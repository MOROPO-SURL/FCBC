namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class GalleryTranslation
    {
        public Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        public Enumerations.GalleryId GalleryId { get; set; }
        public Gallery Gallery { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string About { get; set; }

        public string History { get; set; }
    }
}
