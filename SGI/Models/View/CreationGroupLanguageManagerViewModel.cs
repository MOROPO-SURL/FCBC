namespace MOROPO.FCBC.SGI.Models.View
{
    public class CreationGroupLanguageManagerViewModel
    {
        public CreationGroupDisplayViewModel CreationGroup { get; set; }

        public System.Collections.Generic.ICollection<CreationGroupTranslationDisplayViewModel> Translations { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> NotSelectedLanguage { get; set; }
    }
}
