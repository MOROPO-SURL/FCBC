using MOROPO.FBCB.SGI.Persistence.Entities;
using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class AdministrativeRoleInputViewModel: SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<MOROPO.FBCB.SGI.Persistence.Entities.AdministrativeRole, System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Description")]
        public string Description { get; set; }

        public ICollection<EmployeeAdministrativeRoleBridge> EmployeeAdministrativeRoleBridges { get; set; }

        public MOROPO.FBCB.SGI.Persistence.Entities.AdministrativeRole Export()
        {
            var entity = new MOROPO.FBCB.SGI.Persistence.Entities.AdministrativeRole();

            Mergue(entity);

            return entity;
        }

        public void Import(MOROPO.FBCB.SGI.Persistence.Entities.AdministrativeRole entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            EmployeeAdministrativeRoleBridges = entity.EmployeeAdministrativeRoleBridges;
        }

        public void Mergue(MOROPO.FBCB.SGI.Persistence.Entities.AdministrativeRole entity)
        {
            entity.Name = Name;
            entity.Description = Description;
            entity.EmployeeAdministrativeRoleBridges = EmployeeAdministrativeRoleBridges;
        }
    }
}
