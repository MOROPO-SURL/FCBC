using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class InstitutionalServiceTranslationInputViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "InstitutionalService")]
        public InstitutionalService InstitutionalService { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public InstitutionalServiceLanguageDisplayViewModel InstitutionalServiceLanguege { get; set; }
    }
}
