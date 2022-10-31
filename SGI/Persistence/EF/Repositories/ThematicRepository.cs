using Microsoft.EntityFrameworkCore;
using MOROPO.FBCB.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ThematicRepository: AbstractEntityRepository<Thematic, System.Guid>
    {
        public ThematicRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Thematic, object> GetPagingOrderSelector()
        {
            return entity => entity.Name ?? entity.Description;

        }
    }
}
