namespace MOROPO.FCBC.SGI.Models.View
{
    public class ServiceWorkLanguageManagerViewModel
    {
        public ServiceWorkDisplayViewModel Work { get; set; }

        public System.Collections.Generic.ICollection<ServiceWorkTranslationDisplayViewModel> Translations { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> NotSelectedLanguage { get; set; }
    }
}
