using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Core.Persistence.EF.Repositories;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class AwardRepository : AbstractEntityRepository<Award, System.Guid>
    {
        public AwardRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Award, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }

        protected override System.Linq.IQueryable<Award> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(e => e.Country);
        }
    }
}
