using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtisticTechniqueInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<ArtisticTechnique, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public ArtisticTechnique Export()
        {
            var entity = new ArtisticTechnique();

            Mergue(entity);

            return entity;
        }

        public void Import(ArtisticTechnique entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }

        public void Mergue(ArtisticTechnique entity)
        {
            entity.Name = Name;
            entity.Description = Description;
        }
    }
}