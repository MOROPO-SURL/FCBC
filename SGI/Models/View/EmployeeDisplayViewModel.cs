using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Extensions;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class EmployeeDisplayViewModel: SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<FBCB.SGI.Persistence.Entities.Employee, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.Guid ImageId { get; set; }

        public string ImageExtension { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public System.DateTime BornDate { get; set; }

        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex Sex { get; set; }

        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Enumerations.CivilStatus CivilStatus { get; set; }

        public string Alias { get; set; }

        public string About { get; set; }

        public ICollection<EmailContactItem> EmailContactItems { get; set; }

        public ICollection<PhoneContactItem> PhoneContactItems { get; set; }

        public ICollection<EmployeeAdministrativeRoleBridge> EmployeeAdministrativeRoleBridges { get; set; }

        public void Import(FBCB.SGI.Persistence.Entities.Employee entity)
        {
            Id = entity.Id;
            ImageId = entity.PersonIdentity.ImageId ?? System.Guid.Empty;
            ImageExtension = entity.PersonIdentity.Image?.ContentType.ContentTypeToExtension() ?? null;
            Name = entity.PersonIdentity.Name;
            LastName = entity.PersonIdentity.LastName;
            BornDate = entity.PersonIdentity.BornDate;
            Sex = entity.PersonIdentity.Sex;
            CivilStatus = entity.PersonIdentity.CivilStatus;
            Alias = entity.PersonIdentity.Alias;
            About = entity.PersonIdentity.About;            
            EmployeeAdministrativeRoleBridges = entity.EmployeeAdministrativeRoleBridges;
        }
    }
}
