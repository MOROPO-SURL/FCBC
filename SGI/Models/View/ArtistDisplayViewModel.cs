using MOROPO.FCBC.SGI.Extensions;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Persistence.Enumerations;
using MOROPO.SAPRESSC.Middleware.Identity.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistDisplayViewModel: SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Artist, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.Guid ImageId { get; set; }

        public string ImageExtension { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Display(Name = "BornDate")]
        public System.DateTime? BornDate { get; set; }

        [Display(Name = "Sex")]
        public SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex Sex { get; set; }

        [Display(Name = "CivilStatus")]
        public SAPRESSC.Middleware.Identity.Persistence.Enumerations.CivilStatus CivilStatus { get; set; }

        [Display(Name = "Stage name")]
        public string Alias { get; set; }

        [Display(Name = "Observations")]
        public string About { get; set; }

        [Display(Name = "Address")]
        public string AddressPostal { get; set; }

        [Display(Name = "State")]
        public State State { get; set; }

        [Display(Name = "Municipality")]
        public Municipality Municipality { get; set; }

        public ICollection<EmailContactItem> EmailContactItems { get; set; }

        public ICollection<PhoneContactItem> PhoneContactItems { get; set; }

        [Display(Name = "Identity Number")]
        public string CitizenId { get; set; }

        [Display(Name = "Workplace")]
        public WorkPlace Workplace { get; set; }

        [Display(Name = "SchoolLevel")]
        public SchoolLevel SchoolLevel { get; set; }

        public ICollection<ArtistArtisticManifestationBridge> ArtistManifestationBridges { get; set; }
        public ICollection<ArtistCraftManifestationBridge> CraftManifestationBridges { get; set; }
        public ICollection<ArtistDesignManifestationBridge> DesignManifestationBridges { get; set; }
        public ICollection<ArtistServiceLineBridge> ServiceLineBridges { get; set; }
        
        public ValorationDisplayViewModel[] Valorations { get; set; }

        public ICollection<ArtistAward> ArtistAwards { get; set; }

        [Display(Name = "specify training")]
        public string SpecificEducation { get; set; }

        public string AdmitionModeContext { get; set; }

        [Display(Name = "AdmitionMode")]
        public AdmitionMode AdmitionMode { get; set; }

        public System.Guid? AssociationId { get; set; }

        public System.Guid? ResolutionId { get; set; }

        public AssociationDisplayViewModel Association { get; set; }

        public ResolutionDisplayViewModel Resolution { get; set; }

        public CreationGroupMemberDisplayViewModel CreationGroupMember { get; set; }

        [Display(Name = "RegistrationNumber")]        
        public int RegistrationNumber { get; set; }

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
            CraftManifestationBridges = entity.CraftManifestationBridges;
            DesignManifestationBridges = entity.DesignManifestationBridges;
            ServiceLineBridges = entity.ServiceLineBridges;
            ArtistAwards = entity.ArtistAwards;
            RegistrationNumber = entity.RegistrationNumber;
        }
    }
}
