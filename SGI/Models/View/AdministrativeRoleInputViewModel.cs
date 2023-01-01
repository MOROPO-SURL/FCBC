using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class AdministrativeRoleInputViewModel: SAPRESSC.Core.Persistence.Models.IEntityInputModel<AdministrativeRole, System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Description")]
        public string Description { get; set; }

        public AdministrativeRole Export()
        {
            var entity = new AdministrativeRole();

            Mergue(entity);

            return entity;
        }

        public void Import(AdministrativeRole entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }

        public void Mergue(AdministrativeRole entity)
        {
            entity.Name = Name;
            entity.Description = Description;
        }
    }
}
