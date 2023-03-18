

using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CreationGroupMembersInputViewModel
    {
        public System.Guid CreationGroupId { get; set; }

        [Display(Name = "Artistas")]
        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> NotMembers { get; set; } = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

        [Required(ErrorMessage = "RequiredArtistValidationErrorMessage")]
        [Display(Name = "ArtistId")]
        public System.Guid ArtistId { get; set; }
    }
}
