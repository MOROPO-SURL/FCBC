namespace MOROPO.FCBC.SGI.Models.View
{
    public class GalleryLanguageManagerViewModel
    {
        public GalleryDisplayViewModel Gallery { get; set; }

        public System.Collections.Generic.ICollection<GalleryTranslationDisplayViewModel> Translations { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> NotSelectedLanguage { get; set; }
    }
}
