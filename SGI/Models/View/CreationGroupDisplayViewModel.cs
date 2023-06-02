using MOROPO.FCBC.SGI.Extensions;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CreationGroupDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<CreationGroup, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "RegistratedAt")]
        public System.DateTime RegistratedAt { get; set; }

        [Display(Name = "RegistrationEndAt")]
        public System.DateTime RegistrationEndAt { get; set; }

        [Display(Name = "Fecha de Creación")]
        public System.DateTime? CreatedAt { get; set; }

        [Display(Name = "RegistrationNumber")]
        public int RegistrationNumber { get; set; }

        [Display(Name = "State")]
        public State State { get; set; }

        [Display(Name = "Municipality")]
        public Municipality Municipality { get; set; }

        [Display(Name = "Agreement")]
        public Agreement Agreement { get; set; }

        [Display(Name = "Address")]
        public string AddressPostal { get; set; }

        public ICollection<CreationGroupArtisticManifestationBridge> CreationGroupArtisticManifestationBrigdes { get; set; } = new List<CreationGroupArtisticManifestationBridge>();
        public ICollection<CreationGroupAward> CreationGroupAwards { get; set; } = new List<CreationGroupAward>();
        public ICollection<CreationGroupDesignManifestationBridge> DesignManifestationBridges { get; set; } = new List<CreationGroupDesignManifestationBridge>();
        public ICollection<CreationGroupCraftManifestationBridge> CraftManifestationBridges { get; set; } = new List<CreationGroupCraftManifestationBridge>();
        public ICollection<CreationGroupServiceLineBridge> ServiceManifestationBridges { get; set; } = new List<CreationGroupServiceLineBridge>();

        public CriterialDisplayViewModel[] Valorations { get; set; }

        public void Import(CreationGroup entity)
        {
            Id = entity.Id;
            ImageId = entity.Identity.ImageId;
            ImageExtension = entity.Identity.Image?.ContentType.ContentTypeToExtension() ?? null;
            Name = entity.Identity.Name;
            Brand = entity.Brand;
            CreationGroupArtisticManifestationBrigdes = entity.ArtisticManifestationBrigdes;
            CreationGroupAwards = entity.CreationGroupAwards;
            DesignManifestationBridges = entity.DesignManifestationBridges;
            CraftManifestationBridges = entity.CraftManifestationBridges;
            ServiceManifestationBridges = entity.ServiceLineBridges;
            CreatedAt = entity.Identity.BornDate;
            RegistrationNumber = entity.RegistrationNumber;
            RegistratedAt = entity.RegistratedAt;
            RegistrationEndAt = entity.RegistrationEndAt;
            var region = entity.Identity.OrganizationPostalContactItemBridges.FirstOrDefault().ContactItem.Region;
            Municipality = ((MunicipalityRegion)region).Municipality;            
            Agreement = entity.Agreement;
            AddressPostal = entity.Identity.OrganizationPostalContactItemBridges.Select(b => b.ContactItem.Address).First();
        }
    }
}
