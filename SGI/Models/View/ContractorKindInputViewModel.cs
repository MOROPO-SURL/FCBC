using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ContractorKindInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<ContractorKind, int>
    {
        public int Id { get ; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public ContractorKind Export()
        {
            var entity = new ContractorKind();

            Mergue(entity);

            return entity;
        }

        public void Import(ContractorKind entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }

        public void Mergue(ContractorKind entity)
        {
            entity.Name = Name;
            entity.Description = Description;
        }
    }
}
