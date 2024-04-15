using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class WorkLanguagesInputViewModel
    {
        public System.Guid WorkId { get; set; }
        [Display(Name = "Languages")]
        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> NotSelectedLanguages { get; set; } = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

        [Required(ErrorMessage = "RequiredArtistValidationErrorMessage")]
        [Display(Name = "Language")]
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Collection")]
        public string Collection { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }
    }
}
