using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CriticTranslationDisplayViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "Critic")]
        public Critic Critic { get; set; }
        [System.ComponentModel.DataAnnotations.Required]        
        public string About { get; set; }
        public CriticLanguageDisplayViewModel CriticLanguege { get; set; }

        public void Import(CriticTranslation entity)
        {
            Critic = entity.Critic;           
            About = entity.About;
            TranslatableLanguage = entity.TranslatableLanguage;
        }

        public CriticTranslationInputViewModel MergeViewModel()
        {
            CriticTranslationInputViewModel viewModel = new CriticTranslationInputViewModel();
           
            viewModel.About = About;
            viewModel.Critic = Critic;
            viewModel.TranslatableLanguage = TranslatableLanguage;            
            return viewModel;
        }
    }
}
