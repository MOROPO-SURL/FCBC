using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class LanguagesInputViewModel
    {
        public System.Guid WorkId { get; set; }
        
        [Required(ErrorMessage = "RequiredArtistValidationErrorMessage")]
        [System.ComponentModel.DataAnnotations.Display(Name = "Language")]
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
    }
}
