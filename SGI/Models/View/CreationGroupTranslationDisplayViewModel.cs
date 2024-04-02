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
        public CreationGroupLanguageDisplayViewModel CreationGroupLanguege { get; set; }

        public void Import(CreationGroupTranslation entity)
        {
            CreationGroup = entity.CreationGroup;
            Name = entity.Name;
            TranslatableLanguage = entity.TranslatableLanguage;
        }

        public CreationGroupTranslationInputViewModel MergeViewModel()
        {
            CreationGroupTranslationInputViewModel viewModel = new CreationGroupTranslationInputViewModel();
            viewModel.Name = Name;
            viewModel.CreationGroup = CreationGroup;
            viewModel.TranslatableLanguage = TranslatableLanguage;
            viewModel.CreationGroup = CreationGroup;
            return viewModel;
        }
    }
}
