namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtisticTechniqueTranslation
    {
        public Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        public System.Guid ArtisticTechniqueId { get; set; }

        public ArtisticTechnique ArtisticTechnique { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
