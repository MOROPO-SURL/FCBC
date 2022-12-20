using MOROPO.FCBC.SGI.Extensions;
using MOROPO.FCBC.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class EmployeeDisplayViewModel: SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Employee, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.Guid ImageId { get; set; }

        public string ImageExtension { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Display(Name = "BornDate")]
        public System.DateTime BornDate { get; set; }

        [Display(Name = "Sex")]
        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex Sex { get; set; }

        [Display(Name = "CivilStatus")]
        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Enumerations.CivilStatus CivilStatus { get; set; }

        public string Alias { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        [Display(Name = "Address")]
        public string AddressPostal { get; set; }

        [Display(Name = "State")]
        public State State { get; set; }

        [Display(Name = "Municipality")]
        public Municipality Municipality { get; set; }

        public ICollection<EmailContactItem> EmailContactItems { get; set; }

        public ICollection<PhoneContactItem> PhoneContactItems { get; set; }

        public ICollection<EmployeeAdministrativeRoleBridge> EmployeeAdministrativeRoleBridges { get; set; }

        public void Import(Employee entity)
        {
            Id = entity.Id;
            ImageId = entity.Identity.ImageId ?? System.Guid.Empty;
            ImageExtension = entity.Identity.Image?.ContentType.ContentTypeToExtension() ?? null;
            Name = entity.Identity.Name;
            LastName = entity.Identity.LastName;
            BornDate = entity.Identity.BornDate;
            Sex = entity.Identity.Sex;
            CivilStatus = entity.Identity.CivilStatus;
            Alias = entity.Identity.Alias;
            About = entity.Identity.About;            
            EmployeeAdministrativeRoleBridges = entity.AdministrativeRoleBridges;
            AddressPostal = entity.Identity.PersonPostalContactItemBridges.Select(b => b.ContactItem.Address).First();
            var region = entity.Identity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.Region;
            Municipality = ((MunicipalityRegion)region).Municipality;
            State = Municipality.State;
        }
    }
}
