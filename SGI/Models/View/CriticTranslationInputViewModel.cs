using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CriticTranslationInputViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
        [Display(Name = "Critic")]
        public Critic Critic { get; set; }
        public string About { get; set; }
        public CriticLanguageDisplayViewModel CriticLanguege { get; set; }
    }
}
