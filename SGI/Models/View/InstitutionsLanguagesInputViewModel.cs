using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class InstitutionsLanguagesInputViewModel
    {
        public System.Guid GeneralId { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Language")]
        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> NotSelectedLanguages { get; set; } = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

        [Required(ErrorMessage = "RequiredArtistValidationErrorMessage")]
        [System.ComponentModel.DataAnnotations.Display(Name = "Language")]
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
    }
}
