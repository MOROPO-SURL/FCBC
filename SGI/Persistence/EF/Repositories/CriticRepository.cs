using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class CriticRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Critic, System.Guid>
    {
        public CriticRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Critic, object> GetPagingOrderSelector()
        {
            return entity => entity.Identity.Name;
        }

        protected override System.Linq.IQueryable<Critic> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(e => e.Identity).ThenInclude(p => p.Image).Include(e => e.Translations);
        }
    }
}
