using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class CreationGroupRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<CreationGroup, System.Guid>
    {
        public CreationGroupRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<CreationGroup, object> GetPagingOrderSelector()
        {
            return entity => entity.Identity.Name;
        }

        protected override System.Linq.IQueryable<CreationGroup> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(e => e.Identity).ThenInclude(e => e.Image)
                .Include(p => p.Identity).ThenInclude(p => p.OrganizationPhoneContactItemBridges).ThenInclude(p => p.ContactItem)
                .Include(p => p.Identity).ThenInclude(p => p.OrganizationEmailContactItemBridges).ThenInclude(p => p.ContactItem)
                .Include(p => p.Identity).ThenInclude(p => p.OrganizationSocialContactItemBridges).ThenInclude(p => p.ContactItem)
                .Include(p => p.Identity).ThenInclude(p => p.OrganizationPostalContactItemBridges).ThenInclude(p => p.ContactItem).ThenInclude(p => p.Region).ThenInclude(p => ((MunicipalityRegion)p).Municipality).ThenInclude(p => p.State)
                .Include(e => e.ArtisticManifestationBrigdes).ThenInclude(e => e.Manifestation)
                .Include(e => e.ArtistBridges).ThenInclude(e => e.Artist)
                                                           .ThenInclude(e => e.Identity)
                                                           .ThenInclude(e => e.Image)
                .Include(e => e.ArtistBridges).ThenInclude(e => e.Artist)
                                                           .ThenInclude(e => e.Identity)
                                                           .ThenInclude(e => e.PersonPostalContactItemBridges)
                                                           .ThenInclude(e => e.ContactItem)
                .Include(e => e.ArtistBridges).ThenInclude(e => e.Artist)
                                                           .ThenInclude(e => e.Identity)
                                                           .ThenInclude(e => e.PersonEmailContactItemBridges)
                                                           .ThenInclude(e => e.ContactItem)
                .Include(e => e.ArtistBridges).ThenInclude(e => e.Artist)
                                                           .ThenInclude(e => e.Identity)
                                                           .ThenInclude(e => e.PersonPhoneContactItemBridges)
                                                           .ThenInclude(e => e.ContactItem)
                .Include(e => e.CreationGroupAwards).ThenInclude(e => e.Award).ThenInclude(p => p.Country)
                .Include(e => e.CreationGroupDistinctionBridges).ThenInclude(e => e.Distinction)
                .Include(e => e.CreationGroupRecognitionBridges).ThenInclude(e => e.Recognition)
                .Include(e => e.CraftManifestationBridges).ThenInclude(e => e.Manifestation)
                .Include(e => e.DesignManifestationBridges).ThenInclude(e => e.Manifestation)
                .Include(e => e.ServiceLineBridges).ThenInclude(e => e.Line)
                .Include(p => p.Attachments)
                .Include(p => p.Agreement);
        }
    }
}
