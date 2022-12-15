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
            return base.GetQueryableDbSet().Include(e => e.Identity).ThenInclude(e => e.Image).Include( e => e.ArtisticManifestationBrigdes).ThenInclude( e => e.Manifestation);
        }
    }
}
