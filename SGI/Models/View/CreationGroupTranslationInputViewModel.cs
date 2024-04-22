using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CreationGroupTranslationInputViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "CreationGroup")]
        public CreationGroup CreationGroup { get; set; }
        [System.ComponentModel.DataAnnotations.Required]        
        public string Name { get; set; }
        public string Brand { get; set; }
        public string About { get; set; }
        public CreationGroupLanguageDisplayViewModel CreationGroupLanguege { get; set; }
    }
}
