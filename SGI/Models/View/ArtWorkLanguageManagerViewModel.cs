using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtWorkLanguageManagerViewModel
    {
        public ArtWorkDisplayViewModel Work { get; set; }

        public System.Collections.Generic.ICollection<ArtWorkTranslationDisplayViewModel> Translations { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> NotSelectedLanguage { get; set; }
    }
}
