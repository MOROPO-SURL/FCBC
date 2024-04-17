namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CraftManifestationTranslation
    {
        public Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        public System.Guid CraftManifestationId { get; set; }

        public CraftManifestation CraftManifestation { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }

    }
}
