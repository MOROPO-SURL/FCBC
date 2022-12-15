
namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class CreationGroup : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("OrganizationIdentityId")]
        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities.OrganizationIdentity OrganizationIdentity { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid OrganizationIdentityId { get; set; }

        public System.Collections.Generic.ICollection<CreationGroupArtisticManifestationBrigde> CreationGroupArtisticManifestationBrigdes { get; set; }

        public System.Guid[] SelectedValues { get; set; }

        public System.Collections.Generic.ICollection<CreationGroupArtistBridge> CreationGroupArtistBridges { get; set; }

        public CreationGroup()
        {
            Id = System.Guid.NewGuid();
            OrganizationIdentity = new SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities.OrganizationIdentity();
            CreationGroupArtistBridges = new System.Collections.Generic.List<CreationGroupArtistBridge>();
        }
    }
}
