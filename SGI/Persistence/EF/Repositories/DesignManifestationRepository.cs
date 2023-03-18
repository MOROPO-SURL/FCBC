using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class DesignManifestationRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.DesignManifestation, System.Guid>
    {
        public DesignManifestationRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.DesignManifestation, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }

        protected override System.Linq.IQueryable<DesignManifestation> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(c => c.Group);
        }
    }
}
