using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class MaterialTranslationInputViewModel
    {
        [System.ComponentModel.DataAnnotations.Display(Name = "Language")]
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "Material")]
        public Material Material { get; set; }
        
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public MaterialLanguageDisplayViewModel MaterialLanguege { get; set; }
    }
}
