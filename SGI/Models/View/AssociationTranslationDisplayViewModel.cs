using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class AssociationTranslationDisplayViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "Association")]
        public Association Association { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public AssociationLanguageDisplayViewModel AssociationLanguege { get; set; }

        public void Import(AssociationTranslation entity)
        {
            Association = entity.Association;
            Name = entity.Name;
            TranslatableLanguage = entity.TranslatableLanguage;
        }

        public AssociationTranslationInputViewModel MergeViewModel()
        {
            AssociationTranslationInputViewModel viewModel = new AssociationTranslationInputViewModel();
            viewModel.Name = Name;
            viewModel.Association = Association;
            viewModel.TranslatableLanguage = TranslatableLanguage;
            viewModel.AssociationLanguege = AssociationLanguege;
            return viewModel;
        }
    }
}
