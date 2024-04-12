using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class WorkLanguagesInputViewModel
    {
        public System.Guid WorkId { get; set; }
        [Display(Name = "Languages")]
        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> NotSelectedLanguages { get; set; } = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

        [Required(ErrorMessage = "RequiredArtistValidationErrorMessage")]
        [Display(Name = "Translation")]
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
        public string Name { get; set; }
        public string Collection { get; set; }
        public string Location { get; set; }
    }
}
