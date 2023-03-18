using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class AdministrativeRoleDisplayViewModel: SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<AdministrativeRole, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public void Import(AdministrativeRole entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
