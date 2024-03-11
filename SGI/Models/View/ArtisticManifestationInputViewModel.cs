using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtisticManifestationInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<ArtisticManifestation, System.Guid>
    {
        public System.Guid Id { get ; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Description")]
        public string Description { get; set; }

        public ArtisticManifestation Export()
        {
            var entity = new ArtisticManifestation();

            Mergue(entity);

            return entity;
        }

        public void Import(ArtisticManifestation entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }

        public void Mergue(ArtisticManifestation entity)
        {
            entity.Name = Name;
            entity.Description = Description;
        }
    }
}
