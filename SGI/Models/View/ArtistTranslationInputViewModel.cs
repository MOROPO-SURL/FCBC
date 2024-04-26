namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistTranslationInputViewModel
    {
        [System.ComponentModel.DataAnnotations.Display(Name = "Language")]
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        public System.Guid ArtistId { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Display(Name = "Description")]
        public string Description { get; set; }
    }
}
