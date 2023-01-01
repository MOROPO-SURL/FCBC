using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class DesignTechniqueInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<DesignTechnique, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public DesignTechnique Export()
        {
            var entity = new DesignTechnique();

            Mergue(entity);

            return entity;
        }

        public void Import(DesignTechnique entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }

        public void Mergue(DesignTechnique entity)
        {
            entity.Name = Name;
            entity.Description = Description;
        }
    }
}
