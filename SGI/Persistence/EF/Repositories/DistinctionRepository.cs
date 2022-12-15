using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class DistinctionRepository : AbstractEntityRepository<Distinction, System.Guid>
    {
        public DistinctionRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Distinction, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
