namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtisticManifestationTranslation
    {
        public Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        public System.Guid Id { get; set; }
        
        public ArtisticManifestation ArtisticManifestation { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
       
        public string Description { get; set; }
    }
}
