using Microsoft.EntityFrameworkCore;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class CriticRepository : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<MOROPO.FBCB.SGI.Persistence.Entities.Critic, System.Guid>
    {
        public CriticRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<FBCB.SGI.Persistence.Entities.Critic, object> GetPagingOrderSelector()
        {
            return entity => entity.PersonIdentity.Name;
        }

        protected override System.Linq.IQueryable<FBCB.SGI.Persistence.Entities.Critic> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(e => e.PersonIdentity).ThenInclude(p => p.Image);
        }
    }
}
