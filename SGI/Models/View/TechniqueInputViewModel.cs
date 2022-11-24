using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class TechniqueInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<FBCB.SGI.Persistence.Entities.Technique, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public FBCB.SGI.Persistence.Entities.Technique Export()
        {
            var entity = new FBCB.SGI.Persistence.Entities.Technique();

            Mergue(entity);

            return entity;
        }

        public void Import(FBCB.SGI.Persistence.Entities.Technique entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }

        public void Mergue(FBCB.SGI.Persistence.Entities.Technique entity)
        {
            entity.Name = Name;
            entity.Description = Description;
        }
    }
}