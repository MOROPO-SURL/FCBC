using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ServiceWorkTranslationDisplayViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "ServiceWork")]
        public ServiceWork ServiceWork { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public ServiceWorkLanguageDisplayViewModel ServiceWorkLanguege { get; set; }

        public void Import(ServiceWorkTranslation entity)
        {
            ServiceWork = entity.ServiceWork;
            Name = entity.Name;
            TranslatableLanguage = entity.TranslatableLanguage;
        }

        public ServiceWorkTranslationInputViewModel MergeViewModel()
        {
            ServiceWorkTranslationInputViewModel viewModel = new ServiceWorkTranslationInputViewModel();
            viewModel.Name = Name;
            viewModel.ServiceWork = ServiceWork;
            viewModel.TranslatableLanguage = TranslatableLanguage;
            viewModel.ServiceWork = ServiceWork;
            return viewModel;
        }
    }
}
