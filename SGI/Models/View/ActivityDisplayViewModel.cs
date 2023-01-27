using MOROPO.FCBC.SGI.Extensions;
using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ActivityDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Activity, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        [Display(Name = "ActivityKind")]
        public string ActivityKindName { get; set; }

        [Display(Name = "PlanedAt")]
        public System.DateTime? PlanedAt { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public void Import(Activity entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            ImageId = entity.ImageId ?? System.Guid.Empty;
            ImageExtension = entity.FeatureImage?.ContentType.ContentTypeToExtension() ?? null;
            ActivityKindName = entity.ActivityKind.Name;
            PlanedAt = entity.PlanedAt;
            Description = entity.Description;
        }
    }
}
