namespace MOROPO.FCBC.SGI.Models.View
{
    public class ActivityLanguageManagerViewModel
    {
        public ActivityDisplayViewModel Activity { get; set; }

        public System.Collections.Generic.ICollection<ActivityTranslationDisplayViewModel> Translations { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> NotSelectedLanguage { get; set; }
    }
}
