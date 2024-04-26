using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class AssociationTranslationInputViewModel
    {
        [System.ComponentModel.DataAnnotations.Display(Name = "Language")]
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "Association")]
        public Association Association { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public AssociationLanguageDisplayViewModel AssociationLanguege { get; set; }
    }
}
