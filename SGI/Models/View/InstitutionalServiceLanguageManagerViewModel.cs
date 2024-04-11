namespace MOROPO.FCBC.SGI.Models.View
{
    public class InstitutionalServiceLanguageManagerViewModel
    {
        public ServiceDisplayViewModel Service { get; set; }

        public System.Collections.Generic.ICollection<InstitutionalServiceTranslationDisplayViewModel> Translations { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> NotSelectedLanguage { get; set; }
    }
}
