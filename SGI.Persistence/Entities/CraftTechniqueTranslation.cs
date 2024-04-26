
namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CraftTechniqueTranslation
    {
        public Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        public System.Guid CraftTechniqueId { get; set; }

        public CraftTechnique CraftTechnique { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
