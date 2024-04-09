namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistLanguageManagerViewModel
    {
        public ArtistDisplayViewModel Artist { get; set; }

        public System.Collections.Generic.ICollection<ArtistTranslationDisplayViewModel> Translations { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> NotSelectedLanguage { get; set; }
    }
}
