namespace MOROPO.FCBC.SGI.Models.View
{
    public class ExposureTranslationInputViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        public System.Guid ExposureId { get; set; }
        
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Description")]
        public string Description { get; set; }
    }
}
