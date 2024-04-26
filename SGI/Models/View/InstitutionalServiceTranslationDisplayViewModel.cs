using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class InstitutionalServiceTranslationDisplayViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "InstitutionalService")]
        public InstitutionalService InstitutionalService { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public InstitutionalServiceLanguageDisplayViewModel InstitutionalServiceLanguege { get; set; }

        public void Import(InstitutionalServiceTranslation entity)
        {
            InstitutionalService = entity.InstitutionalService;
            Name = entity.Name;
            Description = entity.Description;
            TranslatableLanguage = entity.TranslatableLanguage;
        }

        public InstitutionalServiceTranslationInputViewModel MergeViewModel()
        {
            InstitutionalServiceTranslationInputViewModel viewModel = new InstitutionalServiceTranslationInputViewModel();
            viewModel.Name = Name;  
            viewModel.Description = Description;
            viewModel.InstitutionalService = InstitutionalService;
            viewModel.TranslatableLanguage = TranslatableLanguage;
            viewModel.InstitutionalServiceLanguege = InstitutionalServiceLanguege;
            return viewModel;
        }
    }
}
