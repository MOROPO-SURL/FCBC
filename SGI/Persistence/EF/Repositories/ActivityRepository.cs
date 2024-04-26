using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ActivityRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Activity, System.Guid>
    {
        public ActivityRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Activity, object> GetPagingOrderSelector()
        {
            return entity => entity.Name ?? entity.ActivityKind.Name;
        }

        protected override System.Linq.IQueryable<Activity> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(a => a.ActivityKind).Include(e => e.FeatureImage).Include(entity => entity.Translations);
        }
    }
}
