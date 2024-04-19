using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class LanguagesInputViewModel
    {
        public System.Guid WorkId { get; set; }
        [Required(ErrorMessage = "RequiredArtistValidationErrorMessage")]
        [Display(Name = "Translation")]
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
    }
}
