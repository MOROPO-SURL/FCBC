using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ExposureTranslationDisplayViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "Exposure")]
        public Exposure Exposure { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public ExposureLanguageDisplayViewModel ExposureLanguege { get; set; }

        public void Import(ExposureTranslation entity)
        {
            Exposure = entity.Exposure;
            Name = entity.Name;
            TranslatableLanguage = entity.TranslatableLanguage;
        }

        public ExposureTranslationInputViewModel MergeViewModel()
        {
            ExposureTranslationInputViewModel viewModel = new ExposureTranslationInputViewModel();
            viewModel.Name = Name;
            viewModel.Exposure = Exposure;
            viewModel.TranslatableLanguage = TranslatableLanguage;
            viewModel.ExposureLanguege = ExposureLanguege;
            return viewModel;
        }
    }
}
