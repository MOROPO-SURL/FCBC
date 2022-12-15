
using MOROPO.FBCB.SGI.Persistence.Enumerations;

namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class Artist : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public Artist()
        {
            Id = System.Guid.NewGuid();
            PersonIdentity = new SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity();
            ArtistManifestationBridges = new System.Collections.Generic.List<ArtistManifestationBridge>();
            CreationGroupArtistBridges = new System.Collections.Generic.List<CreationGroupArtistBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("PersonIdentityId")]
        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity PersonIdentity { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid PersonIdentityId { get; set; }

        public string CitizenId { get; set; }

        public Enumerations.WorkPlace Workplace { get; set; }

        public Enumerations.SchoolLevel SchoolLevel { get; set; }

        public string SpecificEducation { get; set; }

        public string Context { get; set; }

        public ModeEntry AdmitionModel { get; set; }

        public System.Collections.Generic.ICollection<ArtistManifestationBridge> ArtistManifestationBridges { get; set; }

        public System.Collections.Generic.ICollection<CreationGroupArtistBridge> CreationGroupArtistBridges { get; set; }
    }
}
