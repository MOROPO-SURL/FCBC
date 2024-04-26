namespace MOROPO.FCBC.SGI.Models.View
{
    public class AssociationLanguageManagerViewModel
    {
        public AssociationDisplayViewModel Association { get; set; }

        public System.Collections.Generic.ICollection<AssociationTranslationDisplayViewModel> Translations { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> NotSelectedLanguage { get; set; }
    }
}
