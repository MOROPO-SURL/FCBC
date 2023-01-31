using MOROPO.FCBC.SGI.Extensions;
using MOROPO.FCBC.SGI.Persistence.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public ICollection<CreationGroupArtisticManifestationBridge> CreationGroupArtisticManifestationBrigdes { get; set; } = new List<CreationGroupArtisticManifestationBridge>();
        public ICollection<CreationGroupAward> CreationGroupAwards { get; set; } = new List<CreationGroupAward>();
        public ICollection<CreationGroupDesignManifestationBridge> DesignManifestationBridges { get; set; } = new List<CreationGroupDesignManifestationBridge>();
        public ICollection<CreationGroupCraftManifestationBridge> CraftManifestationBridges { get; set; } = new List<CreationGroupCraftManifestationBridge>();

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
            CreatedAt = entity.Identity.BornDate;
            RegistrationNumber = entity.RegistrationNumber;
            RegistratedAt = entity.RegistratedAt;
            RegistrationEndAt = entity.RegistrationEndAt;
        }
    }
}
