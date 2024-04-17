namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ServiceLineTranslation
    {
        public Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        public System.Guid ServiceLineId { get; set; }

        public ServiceLine ServiceLine { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
