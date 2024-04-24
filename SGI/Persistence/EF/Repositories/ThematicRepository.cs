using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Core.Persistence.EF.Repositories;
using System.Linq;

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

        protected override IQueryable<Thematic> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(entity => entity.Translations);
        }
    }
}
