using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class DesignManifestationGroupInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<DesignManifestationGroup, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Parent")]
        public System.Guid? ParentId { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public string SerializedTree { get; set; }

        public DesignManifestationGroup Export()
        {
            var entity = new DesignManifestationGroup();

            Mergue(entity);

            return entity;
        }

        public void Import(DesignManifestationGroup entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            ParentId = entity.ParentId;
            Description = entity.Description;
        }

        public void Mergue(DesignManifestationGroup entity)
        {
            entity.Name = Name;
            entity.ParentId = ParentId;
            entity.Description = Description;
        }
    }
}
