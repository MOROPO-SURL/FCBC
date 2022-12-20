using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class CreationGroupRepository : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<CreationGroup, System.Guid>
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
                                                           .ThenInclude(e => e.ContactItem);
        }
    }
}
