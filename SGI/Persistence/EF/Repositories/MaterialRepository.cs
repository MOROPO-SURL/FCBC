using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;

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
    }
}
