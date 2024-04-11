using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class GalleryTranslationInputViewModel
    {
        [Display(Name = "Language")]
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        public Persistence.Enumerations.GalleryId GalleryId { get; set; }
        
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        [Display(Name = "History")]
        public string History { get; set; }
    }
}
