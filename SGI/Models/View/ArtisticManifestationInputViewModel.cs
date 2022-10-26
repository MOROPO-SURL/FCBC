using MOROPO.FBCB.SGI.Persistence.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtisticManifestationInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<ArtisticManifestation, Guid>
    {
        public Guid Id { get ; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

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
        }

        public void Mergue(ArtisticManifestation entity)
        {
            entity.Name = Name;
        }
    }
}
