using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtWorkTranslationInputViewModel
    {
        [System.ComponentModel.DataAnnotations.Display(Name = "Language")]
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "ArtWork")]
        public ArtWork ArtWork { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public ArtWorkLanguageDisplayViewModel ArtWorkLanguege { get; set; }

        public string Collection { get; set; }
        public string Loaction { get; set; }
    }
}
