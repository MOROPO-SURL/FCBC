using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using System.Linq;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class MaterialRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Material, System.Guid>
    {
        public MaterialRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Material, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }

        protected override IQueryable<Material> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(entity => entity.Translations);        
        }

    }
}
