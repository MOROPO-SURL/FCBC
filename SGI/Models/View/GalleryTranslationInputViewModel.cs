using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class GalleryTranslationInputViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "Gallery")]
        public Gallery Gallery { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public GalleryLanguageDisplayViewModel GalleryLanguege { get; set; }
    }
}
