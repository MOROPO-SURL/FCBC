using Microsoft.EntityFrameworkCore;
using MOROPO.FBCB.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories;
using System;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class AwardRepository : AbstractEntityRepository<Award, Guid>
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
