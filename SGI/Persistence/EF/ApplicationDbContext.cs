using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Security.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF
{
    public class ApplicationDbContext :
        SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Context.AbstractTraceableDbContext,
        SmartSolucionesCuba.SAPRESSC.Core.Security.Persistence.EF.Context.ISecurityPersistenceModuleDbContext,
        SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.EF.Context.IIdentityMiddlewarePersistenceDbContext
    {
        public ApplicationDbContext(System.Collections.Generic.IEnumerable<SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade> dbSchemaConfigurationProviders, DbContextOptions options) : base(dbSchemaConfigurationProviders, options)
        {
        }

        //#if DEBUG
        //        public override bool TraceChanges => false;
        //#endif

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserGroupBridge> UserGroupBridges { get; set; }
        public DbSet<GroupRoleBridge> GroupRoleBridges { get; set; }

        public DbSet<Thumbnail> Thumbnails { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Video> Videos { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<CountryRegion> CountryRegions { get; set; }
        public DbSet<StateRegion> StateRegions { get; set; }
        public DbSet<MunicipalityRegion> MunicipalityRegions { get; set; }
        public DbSet<LocalityRegion> LocalityRegions { get; set; }

        public DbSet<PersonIdentity> PersonIdentities { get; set; }
        public DbSet<OrganizationPostalContactItemBridge> OrganizationPostalContactItemBridges { get; set; }
        public DbSet<OrganizationPhoneContactItemBridge> OrganizationPhoneContactItemBridges { get; set; }
        public DbSet<OrganizationEmailContactItemBridge> OrganizationEmailContactItemBridges { get; set; }
        public DbSet<PersonWebSiteContactItemBridge> PersonWebSiteContactItemBridges { get; set; }
        public DbSet<PersonSocialContactItemBridge> PersonSocialContactItemBridges { get; set; }
        public DbSet<PersonPostalContactItemBridge> PersonPostalContactItemBridges { get; set; }
        public DbSet<PersonPhoneContactItemBridge> PersonPhoneContactItemBridges { get; set; }
        public DbSet<PersonEmailContactItemBridge> PersonEmailContactItemBridges { get; set; }
        public DbSet<PostalContactItem> PostalContactItems { get; set; }
        public DbSet<WebSiteContactItem> WebSiteContactItems { get; set; }
        public DbSet<SocialContactItem> SocialContactItems { get; set; }
        public DbSet<PhoneContactItem> PhoneContactItems { get; set; }
        public DbSet<EmailContactItem> EmailContactItems { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<OrganizationIdentity> OrganizationIdentities { get; set; }
        public DbSet<OrganizationSocialContactItemBridge> OrganizationSocialContactItemBridges { get; set; }
        public DbSet<OrganizationWebSiteContactItemBridge> OrganizationWebSiteContactItemBridges { get; set; }
        
        public DbSet<InstitutionalInformation> InstitutionalInformations { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<Distinction> Distinctions { get; set; }
        public DbSet<Recognition> Recognitions { get; set; }
        public DbSet<InstitutionalService> InstitutionalServices { get; set; }
        public DbSet<Thematic> Thematics { get; set; }
        public DbSet<ContractorKind> ContractorKinds { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Critic> Critics { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<CreationGroup> CreationGroups { get; set; }
        public DbSet<Association> Associations { get; set; }
        public DbSet<AdministrativeRole> AdministrativeRoles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAdministrativeRoleBridge> AdministrativeRoleBridges { get; set; }
        public DbSet<Resolution> Resolutions { get; set; }
        public DbSet<ArtisticManifestation> ArtisticManifestations { get; set; }
        public DbSet<CraftManifestation> CraftManifestations { get; set; }
        public DbSet<DesignManifestation> DesignManifestations { get; set; }
        public DbSet<ServiceLine> ServiceLines { get; set; }
        public DbSet<ArtisticManifestationGroup> ArtisticManifestationGroups { get; set; }
        public DbSet<CraftManifestationGroup> CraftManifestationGroups { get; set; }
        public DbSet<DesignManifestationGroup> DesignManifestationGroups { get; set; }
        public DbSet<ServiceLineGroup> ServiceLineGroups { get; set; }
        public DbSet<ArtistArtisticManifestationBridge> ArtistArtisticManifestationBridges { get; set; }
        public DbSet<ArtistCraftManifestationBridge> ArtistCraftManifestationBridges { get; set; }
        public DbSet<ArtistDesignManifestationBridge> ArtistDesignManifestationBridges { get; set; }
        public DbSet<ArtistServiceLineBridge> ArtistServiceLineBridges { get; set; }
        public DbSet<CreationGroupArtisticManifestationBridge> CreationGroupArtisticManifestationBridges { get; set; }
        public DbSet<CreationGroupCraftManifestationBridge> CreationGroupCraftManifestationBridges { get; set; }
        public DbSet<CreationGroupDesignManifestationBridge> CreationGroupDesignManifestationBridges { get; set; }
        public DbSet<CreationGroupServiceLineBridge> CreationGroupServiceLineBridges { get; set; }
        public DbSet<ArtistAttachment> ArtistAttachments { get; set; }
        public DbSet<CreationGroupAttachment> CreationGroupAttachments { get; set; }
        public DbSet<ArtWork> ArtWorks { get; set; }
        public DbSet<CraftWork> CraftWorks { get; set; }
        public DbSet<DesignWork> DesignWorks { get; set; }
        public DbSet<ServiceWork> ServiceWorks { get; set; }
        public DbSet<ArtistAssociationLock> ArtistAssociationLocks { get; set; }
        public DbSet<ArtistResolutionLock> ArtistResolutionLocks { get; set; }
        public DbSet<CreationGroupArtistBridge> CreationGroupArtistBridges { get; set; }
        public DbSet<ArtisticTechnique> ArtisticTechniques { get; set; }
        public DbSet<CraftTechnique> CraftTechniques { get; set; }
        public DbSet<DesignTechnique> DesignTechniques { get; set; }
        public DbSet<ServiceTechnique> ServiceTechniques { get; set; }
        public DbSet<ArtWorkTechniqueBridge> ArtWorkTechniqueBridges { get; set; }
        public DbSet<CraftWorkTechniqueBridge> CraftWorkTechniqueBridges { get; set; }
        public DbSet<DesignWorkTechniqueBridge> DesignWorkTechniqueBridges { get; set; }
        public DbSet<ServiceWorkTechniqueBridge> ServiceWorkTechniqueBridges { get; set; }
        public DbSet<ArtWorkImageBridge> ArtWorkImageBridges { get; set; }
        public DbSet<CraftWorkImageBridge> CraftWorkImageBridges { get; set; }
        public DbSet<DesignWorkImageBridge> DesignWorkImageBridges { get; set; }
        public DbSet<ServiceWorkImageBridge> ServiceWorkImageBridges { get; set; }
        public DbSet<UnitMeasurement> UnitMeasurements { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<ArtWorkMaterialBridge> ArtWorkMaterialBridges { get; set; }
        public DbSet<ArtWorkCreatorBridge> ArtWorkCreatorBridges { get; set; }
        public DbSet<ArtWorkCreationGroupBridge> ArtWorkCreationGroupBridges { get; set; }
        public DbSet<ArtWorkAward> ArtWorkAwards { get; set; }
        public DbSet<ArtWorkAttachment> ArtWorkAttachments { get; set; }
        public DbSet<ArtistAwardBridge> ArtistAwards { get; set; }
    }
}
