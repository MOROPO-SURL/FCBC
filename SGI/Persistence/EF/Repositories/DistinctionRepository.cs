using Microsoft.EntityFrameworkCore;
using MOROPO.FBCB.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories;
using System;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class DistinctionRepository : AbstractEntityRepository<Distinction, Guid>
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
