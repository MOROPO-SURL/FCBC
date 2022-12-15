using MOROPO.FCBC.SGI.Persistence.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class DistinctionInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<Distinction, Guid>
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public Distinction Export()
        {
            var entity = new Distinction();

            Mergue(entity);

            return entity;
        }

        public void Import(Distinction entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }

        public void Mergue(Distinction entity)
        {
            entity.Name = Name;
        }
    }
}
