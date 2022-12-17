using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ServiceTechniqueInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<ServiceTechnique, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public ServiceTechnique Export()
        {
            var entity = new ServiceTechnique();

            Mergue(entity);

            return entity;
        }

        public void Import(ServiceTechnique entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }

        public void Mergue(ServiceTechnique entity)
        {
            entity.Name = Name;
            entity.Description = Description;
        }
    }
}
