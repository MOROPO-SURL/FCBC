using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CreationGroupTranslationDisplayViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "CreationGroup")]
        public CreationGroup CreationGroup { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }       
        public string About { get; set; }
        public CreationGroupLanguageDisplayViewModel CreationGroupLanguege { get; set; }

        public void Import(CreationGroupTranslation entity)
        {
            CreationGroup = entity.CreationGroup;
            Name = entity.Name;           
            About = entity.About;
            TranslatableLanguage = entity.TranslatableLanguage;
        }

        public CreationGroupTranslationInputViewModel MergeViewModel()
        {
            CreationGroupTranslationInputViewModel viewModel = new CreationGroupTranslationInputViewModel();
            viewModel.Name = Name;            
            viewModel.About = About;
            viewModel.CreationGroup = CreationGroup;
            viewModel.TranslatableLanguage = TranslatableLanguage;           
            return viewModel;
        }
    }
}
