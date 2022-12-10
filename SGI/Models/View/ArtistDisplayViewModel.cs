using MOROPO.FCBC.SGI.Extensions;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistDisplayViewModel: SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<FBCB.SGI.Persistence.Entities.Artist, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.Guid ImageId { get; set; }

        public string ImageExtension { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public System.DateTime BornDate { get; set; }

        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex Sex { get; set; }

        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Enumerations.CivilStatus CivilStatus { get; set; }

        [Display(Name = "Stage name")]
        public string Alias { get; set; }

        [Display(Name = "Observations")]
        public string About { get; set; }

        public string AddressPostal { get; set; }

        [Display(Name = "State")]
        public State State { get; set; }

        [Display(Name = "Municipality")]
        public Municipality Municipality { get; set; }

        public ICollection<EmailContactItem> EmailContactItems { get; set; }

        public ICollection<PhoneContactItem> PhoneContactItems { get; set; }

        [Display(Name = "Identity Number")]
        public string CitizenId { get; set; }

        public FBCB.SGI.Persistence.Enumerations.WorkPlace Workplace { get; set; }

        public FBCB.SGI.Persistence.Enumerations.SchoolLevel SchoolLevel { get; set; }

        [Display(Name = "specify training")]
        public string SpecificEducation { get; set; }

        public void Import(FBCB.SGI.Persistence.Entities.Artist entity)
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
            EmailContactItems = entity.PersonIdentity.PersonEmailContactItemBridges.Select(b => b.ContactItem).ToArray();
            PhoneContactItems = entity.PersonIdentity.PersonPhoneContactItemBridges.Select(b => b.ContactItem).ToArray();
            AddressPostal = entity.PersonIdentity.PersonPostalContactItemBridges.Select(b => b.ContactItem.Address).First();
            var region = entity.PersonIdentity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.Region;
            Municipality = ((MunicipalityRegion)region).Municipality;
            State = Municipality.State;
            CitizenId = entity.CitizenId;
            Workplace = entity.Workplace;
            SchoolLevel = entity.SchoolLevel;
            SpecificEducation = entity.SpecificEducation;
        }
    }
}
