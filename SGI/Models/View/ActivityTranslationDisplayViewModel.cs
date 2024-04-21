using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ActivityTranslationDisplayViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "Activity")]
        public Activity Activity { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Description { get; set; }
        public ActivityLanguageDisplayViewModel ActivityLanguege { get; set; }

        public void Import(ActivityTranslation entity)
        {
            Activity = entity.Activity;
            Description = entity.Description;
            TranslatableLanguage = entity.TranslatableLanguage;
        }

        public ActivityTranslationInputViewModel MergeViewModel()
        {
            ActivityTranslationInputViewModel viewModel = new ActivityTranslationInputViewModel();
            viewModel.Description = Description;
            viewModel.Activity = Activity;
            viewModel.TranslatableLanguage = TranslatableLanguage;
            viewModel.ActivityLanguege = ActivityLanguege;
            return viewModel;
        }
    }
}
