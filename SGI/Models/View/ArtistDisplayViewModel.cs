using MOROPO.FCBC.SGI.Extensions;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Persistence.Enumerations;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistDisplayViewModel: SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Artist, System.Guid>
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

        public WorkPlace Workplace { get; set; }

        public SchoolLevel SchoolLevel { get; set; }

        public ICollection<ArtistArtisticManifestationBridge> ArtistManifestationBridges { get; set; }

        [Display(Name = "specify training")]
        public string SpecificEducation { get; set; }

        public string AdmitionModeContext { get; set; }

        public AdmitionMode AdmitionMode { get; set; }

        public System.Guid? AssociationId { get; set; }

        public System.Guid? ResolutionId { get; set; }

        public AssociationDisplayViewModel Association { get; set; }

        public ResolutionDisplayViewModel Resolution { get; set; }

        public void Import(Artist entity)
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
            EmailContactItems = entity.Identity.PersonEmailContactItemBridges.Select(b => b.ContactItem).ToArray();
            PhoneContactItems = entity.Identity.PersonPhoneContactItemBridges.Select(b => b.ContactItem).ToArray();
            AddressPostal = entity.Identity.PersonPostalContactItemBridges.Select(b => b.ContactItem.Address).First();
            var region = entity.Identity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.Region;
            Municipality = ((MunicipalityRegion)region).Municipality;
            State = Municipality.State;
            CitizenId = entity.CitizenId;
            Workplace = entity.Workplace;
            SchoolLevel = entity.SchoolLevel;
            SpecificEducation = entity.SpecificEducation;
            AdmitionMode = entity.AdmitionMode;
            AdmitionModeContext = entity.AdmitionModeContext;
            ArtistManifestationBridges = entity.ArtisticManifestationBridges;
        }
    }
}
