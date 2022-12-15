using Microsoft.EntityFrameworkCore;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class CreationGroupRepository : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<FBCB.SGI.Persistence.Entities.CreationGroup, System.Guid>
    {
        public CreationGroupRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<FBCB.SGI.Persistence.Entities.CreationGroup, object> GetPagingOrderSelector()
        {
            return entity => entity.OrganizationIdentity.Name;
        }

        protected override System.Linq.IQueryable<FBCB.SGI.Persistence.Entities.CreationGroup> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(e => e.OrganizationIdentity).ThenInclude(e => e.Image)
                .Include(e => e.CreationGroupArtisticManifestationBrigdes).ThenInclude(e => e.ArtisticManifestation)
                .Include(e => e.CreationGroupArtistBridges).ThenInclude(e => e.Artist)
                                                           .ThenInclude(e => e.PersonIdentity)
                                                           .ThenInclude(e => e.Image)
                .Include(e => e.CreationGroupArtistBridges).ThenInclude(e => e.Artist)
                                                           .ThenInclude(e => e.PersonIdentity)
                                                           .ThenInclude(e => e.PersonPostalContactItemBridges)
                                                           .ThenInclude(e => e.ContactItem)
                .Include(e => e.CreationGroupArtistBridges).ThenInclude(e => e.Artist)
                                                           .ThenInclude(e => e.PersonIdentity)
                                                           .ThenInclude(e => e.PersonEmailContactItemBridges)
                                                           .ThenInclude(e => e.ContactItem)
                .Include(e => e.CreationGroupArtistBridges).ThenInclude(e => e.Artist)
                                                           .ThenInclude(e => e.PersonIdentity)
                                                           .ThenInclude(e => e.PersonPhoneContactItemBridges)
                                                           .ThenInclude(e => e.ContactItem);
        }
    }
}
