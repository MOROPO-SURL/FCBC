using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class MaterialTranslationDisplayViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "Material")]
        public Material Material { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public MaterialLanguageDisplayViewModel MaterialLanguege { get; set; }

        public void Import(MaterialTranslation entity)
        {
            Material = entity.Material;
            Name = entity.Name;
            Description = entity.Description;
            TranslatableLanguage = entity.TranslatableLanguage;
        }

        public MaterialTranslationInputViewModel MergeViewModel()
        {
            MaterialTranslationInputViewModel viewModel = new MaterialTranslationInputViewModel();
            viewModel.Name = Name;
            viewModel.Description = Description;
            viewModel.TranslatableLanguage = TranslatableLanguage;
            return viewModel;
        }
    }
}
