using Microsoft.EntityFrameworkCore;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ArtistRepository: SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<MOROPO.FBCB.SGI.Persistence.Entities.Artist, System.Guid>
    {
        public ArtistRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<FBCB.SGI.Persistence.Entities.Artist, object> GetPagingOrderSelector()
        {
            return entity => entity.PersonIdentity.Name;
        }

        protected override System.Linq.IQueryable<FBCB.SGI.Persistence.Entities.Artist> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(e => e.PersonIdentity).ThenInclude(p => p.Image);
        }
    }
}
