using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class RecognitionInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<Recognition, Guid>
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public Recognition Export()
        {
            var entity = new Recognition();

            Mergue(entity);

            return entity;
        }

        public void Import(Recognition entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }

        public void Mergue(Recognition entity)
        {
            entity.Name = Name;
            entity.Description = Description;
        }
    }
}
