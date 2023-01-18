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

        [Display(Name = "Organization")]
        public string OrganizationIdentityName { get; set; }

        [Display(Name = "Fecha en que expira")]
        public System.DateTime? RegistrationEndAt { get; set; }

        [Display(Name = "Fecha de Creación")]
        public System.DateTime? CreateDate { get; set; } = System.DateTime.Now.Date.AddYears(-16);

        [Display(Name = "Número de registro del creador")]
        public int RegistrationNumber { get; set; }

        public ICollection<CreationGroupArtisticManifestationBridge> CreationGroupArtisticManifestationBrigdes { get; set; } = new List<CreationGroupArtisticManifestationBridge>();
        public ICollection<CreationGroupAward> CreationGroupAwards { get; set; } = new List<CreationGroupAward>();
        public ICollection<CreationGroupDesignManifestationBridge> DesignManifestationBridges { get; set; } = new List<CreationGroupDesignManifestationBridge>();
        public ICollection<CreationGroupCraftManifestationBridge> CraftManifestationBridges { get; set; } = new List<CreationGroupCraftManifestationBridge>();

        public void Import(CreationGroup entity)
        {
            Id = entity.Id;
            ImageId = entity.Identity.ImageId ?? System.Guid.Empty;
            ImageExtension = entity.Identity.Image?.ContentType.ContentTypeToExtension() ?? null;
            OrganizationIdentityName = entity.Identity.Name;
            CreationGroupArtisticManifestationBrigdes = entity.ArtisticManifestationBrigdes;
            CreationGroupAwards = entity.CreationGroupAwards;
            DesignManifestationBridges = entity.DesignManifestationBridges;
            CraftManifestationBridges = entity.CraftManifestationBridges;
            CreateDate = entity.Identity.BornDate;
            RegistrationEndAt = entity.RegistrationEndAt;
        }
    }
}
