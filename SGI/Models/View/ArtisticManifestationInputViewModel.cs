using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtisticManifestationInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<ArtisticManifestation, System.Guid>
    {
        public System.Guid Id { get ; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public System.Guid? GroupId { get; set; }

        public string SerializedTree { get; set; }

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
            GroupId = entity.GroupId;
        }

        public void Mergue(ArtisticManifestation entity)
        {
            entity.Name = Name;
            entity.GroupId = GroupId;
        }
    }
}
