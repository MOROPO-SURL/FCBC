using MOROPO.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class VideoInputViewModel
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [FormFileContentTypes(ContentTypes = new string[] { "video/mp4", "video/quicktime", "video/x-msvideo", "video/x-flv", "video/x-ms-wmv", "video/x-matroska", "video/avi" }, ErrorMessage = "Video_FileAllowedOnlyMessage")]
        [Display(Name = "File")]
        public Microsoft.AspNetCore.Http.IFormFile File { get; set; }

        [Display(Name = "IsPublic")]
        public bool IsPublic { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public void Mergue(Persistence.Entities.Contract.AbstractAttachment entity)
        {
            entity.Name = Name;
            entity.IsPublic = IsPublic;
            entity.Description = Description;
        }
    }
}
