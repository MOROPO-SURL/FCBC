using Microsoft.EntityFrameworkCore;
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
        
        public DbSet<FBCB.SGI.Persistence.Entities.ArtisticManifestation> ArtisticManifestations { get; set; }
        public DbSet<FBCB.SGI.Persistence.Entities.Award> Awards { get; set; }
        public DbSet<FBCB.SGI.Persistence.Entities.Distinction> Distinctions { get; set; }
        public DbSet<FBCB.SGI.Persistence.Entities.Recognition> Recognitions { get; set; }
        public DbSet<FBCB.SGI.Persistence.Entities.Service> Services { get; set; }
        public DbSet<FBCB.SGI.Persistence.Entities.Thematic> Thematics { get; set; }
        public DbSet<FBCB.SGI.Persistence.Entities.Technique> Technique { get; set; }
        public DbSet<FBCB.SGI.Persistence.Entities.ContractorKind> ContractorKinds { get; set; }
        public DbSet<FBCB.SGI.Persistence.Entities.Currency> Currencies { get; set; }
        public DbSet<FBCB.SGI.Persistence.Entities.Critic> Critics { get; set; }
        public DbSet<FBCB.SGI.Persistence.Entities.Contractor> Contractors { get; set; }
        public DbSet<FBCB.SGI.Persistence.Entities.Artist> Artists { get; set; }
        public DbSet<FBCB.SGI.Persistence.Entities.CreationGroup> CreationGroups { get; set; }
        public DbSet<FBCB.SGI.Persistence.Entities.Association> Associations { get; set; }
        public DbSet<FBCB.SGI.Persistence.Entities.AdministrativeRole> AdministrativeRoles { get; set; }
        public DbSet<FBCB.SGI.Persistence.Entities.Employee>Employees { get; set; }
        public DbSet<FBCB.SGI.Persistence.Entities.EmployeeAdministrativeRoleBridge> AdministrativeRoleBridges { get; set; }
        public DbSet<FBCB.SGI.Persistence.Entities.Resolution> Resolutions { get; set; }
        public DbSet<FBCB.SGI.Persistence.Entities.ManifestationKind> ManifestationKinds { get; set; }
    }
}
