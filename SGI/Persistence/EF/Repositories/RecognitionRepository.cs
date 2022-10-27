using Microsoft.EntityFrameworkCore;
using MOROPO.FBCB.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories;
using System;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class RecognitionRepository : AbstractEntityRepository<Recognition, Guid>
    {
        public RecognitionRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Recognition, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
