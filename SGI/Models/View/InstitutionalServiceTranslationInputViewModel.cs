using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class InstitutionalServiceTranslationInputViewModel
    {
        [System.ComponentModel.DataAnnotations.Display(Name = "Language")]
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "InstitutionalService")]
        public InstitutionalService InstitutionalService { get; set; }
        
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public InstitutionalServiceLanguageDisplayViewModel InstitutionalServiceLanguege { get; set; }
    }
}
