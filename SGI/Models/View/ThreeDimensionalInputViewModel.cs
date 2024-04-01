using MOROPO.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ThreeDimensionalInputViewModel
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [FormFileContentTypes(ContentTypes = new string[] { "model/vnd.mdl", "application/x-blender", "image/x-3ds", "model/vnd.collada+xml", "application/octet-stream", "application/obj" }, ErrorMessage = "FileAllowedOnlyMessage")]
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
