using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ThematicTranslationDisplayViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "Thematic")]
        public Thematic Thematic { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public ThematicLanguageDisplayViewModel ThematicLanguege { get; set; }

        public void Import(ThematicTranslation entity)
        {
            Thematic = entity.Thematic;
            Name = entity.Name;
            Description = entity.Description;
            TranslatableLanguage = entity.TranslatableLanguage;
        }

        public ThematicTranslationInputViewModel MergeViewModel()
        {
            ThematicTranslationInputViewModel viewModel = new ThematicTranslationInputViewModel();
            viewModel.Name = Name;
            viewModel.Description = Description;            
            viewModel.TranslatableLanguage = TranslatableLanguage;
            return viewModel;
        }
    }
}
