using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ThematicTranslationInputViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "CreationGroup")]
        public CreationGroup CreationGroup { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }       
        public string Description { get; set; }
        public ThematicLanguageDisplayViewModel ThematicLanguege { get; set; }
    }
}
