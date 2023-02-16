using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class Artist : SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public Artist()
        {
            Id = System.Guid.NewGuid();
            Identity = new SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity();
            ArtisticManifestationBridges = new List<ArtistArtisticManifestationBridge>();
            CraftManifestationBridges = new List<ArtistCraftManifestationBridge>();
            DesignManifestationBridges = new List<ArtistDesignManifestationBridge>();
            ServiceLineBridges = new List<ArtistServiceLineBridge>();
            ResolutionLocks = new List<ArtistResolutionLock>();
            AssociationLocks = new List<ArtistAssociationLock>();
            Attachments = new List<ArtistAttachment>();
            ArtistAwards = new List<ArtistAward>();
            ArtistRecognitionBridges = new List<ArtistRecognitionBridge>();
            ArtistDistinctionBridges = new List<ArtistDistinctionBridge>();
            ArtistAssessmentBridges = new List<ArtistAssessmentBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("IdentityId")]
        public SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity Identity { get; set; }
        public System.Guid IdentityId { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.RegularExpression("[0-9]{11}")]
        public string CitizenId { get; set; }

        public string Brand { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("BrandImageId")]
        public SAPRESSC.Middleware.Media.Persistence.Entities.Image BrandImage { get; set; }
        public System.Guid? BrandImageId { get; set; }

        public Enumerations.WorkPlace Workplace { get; set; }

        public Enumerations.SchoolLevel SchoolLevel { get; set; }

        public string SpecificEducation { get; set; }

        public Enumerations.AdmitionMode AdmitionMode { get; set; }

        public string AdmitionModeContext { get; set; }

        public int RegistrationNumber { get; set; }

        public System.DateTime RegistratedAt { get; set; }

        public System.DateTime RegistrationEndAt { get; set; }

        public ICollection<ArtistArtisticManifestationBridge> ArtisticManifestationBridges { get; set; }

        public ICollection<ArtistCraftManifestationBridge> CraftManifestationBridges { get; set; }

        public ICollection<ArtistDesignManifestationBridge> DesignManifestationBridges { get; set; }

        public ICollection<ArtistServiceLineBridge> ServiceLineBridges { get; set; }

        public ICollection<ArtistAssociationLock> AssociationLocks { get; set; }

        public ICollection<ArtistResolutionLock> ResolutionLocks { get; set; }

        public ICollection<ArtistAttachment> Attachments { get; set; }

        public ICollection<CreationGroupArtistBridge> CreationGroupArtistBridges { get; set; }
        
        public ICollection<ArtistAward> ArtistAwards { get; set; }
        
        public ICollection<ArtistRecognitionBridge> ArtistRecognitionBridges { get; set; }
        
        public ICollection<ArtistDistinctionBridge> ArtistDistinctionBridges { get; set; }
        public ICollection<ArtistAssessmentBridge> ArtistAssessmentBridges { get; set; }
    }
}
