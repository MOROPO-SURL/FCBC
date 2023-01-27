using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ActivityKindInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<ActivityKind, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public ActivityKind Export()
        {
            var entity = new ActivityKind();

            Mergue(entity);

            return entity;
        }

        public void Import(ActivityKind entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }

        public void Mergue(ActivityKind entity)
        {
            entity.Name = Name;
            entity.Description = Description;
        }
    }
}
