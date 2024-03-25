using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtWorkLanguagesInputViewModel
    {
        public System.Guid ArtWorkId { get; set; }

        [Display(Name = "Languages")]
        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> NotSelectedLanguages { get; set; } = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

        [Required(ErrorMessage = "RequiredArtistValidationErrorMessage")]
        [Display(Name = "Translation")]
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
    }
}
