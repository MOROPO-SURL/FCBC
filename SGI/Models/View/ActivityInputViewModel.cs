using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using MOROPO.FCBC.SGI.Extensions;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ActivityInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Activity, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [FormFileMaxLength(Length = 3145728, ErrorMessage = "FileLimitSizeExcededMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        public bool ResetImage { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "ActivityKindId")]
        public System.Guid ActivityKindId { get; set; }

        [Display(Name = "ActivityKind")]
        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ActivityKinds { get; set; }

        [Display(Name = "PlanedAt")]
        public System.DateTime? PlanedAt { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public ActivityInputViewModel()
        {
            PlanedAt = System.DateTime.Now.Date.AddDays(1);
        }

        public Activity Export()
        {
            var entity = new Activity();

            Mergue(entity);

            return entity;
        }

        public void Import(Activity entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            ImageId = entity.FeatureImage?.Id ?? System.Guid.Empty;
            ImageExtension = entity.FeatureImage?.ContentType.ContentTypeToExtension() ?? null;
            ActivityKindId = entity.ActivityKindId;
            PlanedAt = entity.PlanedAt;
            Description = entity.Description;
        }

        public void Mergue(Activity entity)
        {
            entity.Name = Name;
            entity.PlanedAt = (System.DateTime)PlanedAt;
            entity.Description = Description;
        }
    }
}
