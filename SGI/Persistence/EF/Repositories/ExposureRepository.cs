using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ExposureRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Exposure, System.Guid>
    {
        public ExposureRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Entities.Exposure> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.EmployeeCuratorshipBridges).ThenInclude(bridge => bridge.Employee).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image);
        }

        public override System.Func<Entities.Exposure, object> GetPagingOrderSelector()
        {
            return entity => entity.CreatedAt;
        }

        public override bool IsPagingOrderDescending()
        {
            return true;
        }
    }
}
