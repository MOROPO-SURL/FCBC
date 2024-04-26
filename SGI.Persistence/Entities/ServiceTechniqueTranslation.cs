
namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ServiceTechniqueTranslation
    {
        public Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        public System.Guid ServiceTechniqueId { get; set; }

        public ServiceTechnique ServiceTechnique { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
