using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CraftWorkTranslationDisplayViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "CraftWork")]
        public CraftWork CraftWork { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public CraftWorkLanguageDisplayViewModel CraftWorkLanguege { get; set; }
        public string Title { get; set; }
        public string About { get; set; }

        public void Import(CraftWorkTranslation entity)
        {
            CraftWork = entity.CraftWork;
            Name = entity.Name;
            TranslatableLanguage = entity.TranslatableLanguage;
        }

        public CraftWorkTranslationInputViewModel MergeViewModel()
        {
            CraftWorkTranslationInputViewModel viewModel = new CraftWorkTranslationInputViewModel();
            viewModel.Name = Name;
            viewModel.CraftWork = CraftWork;
            viewModel.TranslatableLanguage = TranslatableLanguage;
            viewModel.CraftWorkLanguege = CraftWorkLanguege;
            return viewModel;
        }
    }
}
