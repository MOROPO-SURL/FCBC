using MOROPO.SAPRESSC.Middleware.Media.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class Activity : SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        public Activity()
        {
            Id = System.Guid.NewGuid();
            Translations = new System.Collections.Generic.List<ActivityTranslation>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public Image FeatureImage { get; set; }

        public System.Guid? ImageId { get; set; }

        public System.DateTime PlanedAt { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ActivityKindId")]
        public ActivityKind ActivityKind { get; set; }
        public System.Guid ActivityKindId { get; set; }

        public string Description { get; set; }
        public System.Collections.Generic.List<ActivityTranslation> Translations { get; set; }
    }
}
