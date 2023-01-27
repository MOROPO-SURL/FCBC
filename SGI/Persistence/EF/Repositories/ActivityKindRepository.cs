using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ActivityKindRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<ActivityKind, System.Guid>
    {
        public ActivityKindRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<ActivityKind, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
