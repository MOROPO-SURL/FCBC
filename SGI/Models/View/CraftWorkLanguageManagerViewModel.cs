namespace MOROPO.FCBC.SGI.Models.View
{
    public class CraftWorkLanguageManagerViewModel
    {
        public CraftWorkDisplayViewModel Work { get; set; }

        public System.Collections.Generic.ICollection<CraftWorkTranslationDisplayViewModel> Translations { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> NotSelectedLanguage { get; set; }
    }
}
