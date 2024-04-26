namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ExposureTranslation
    {
        public Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        public System.Guid ExposureId { get; set; }
        public Exposure Exposure { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
