using Microsoft.EntityFrameworkCore;
using MOROPO.FBCB.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ManifestationKindRepository: AbstractEntityRepository<ManifestationKind, System.Guid>
    {
        public ManifestationKindRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<ManifestationKind, object> GetPagingOrderSelector()
        {
            return entity => entity.Name ?? entity.Parent.Name;
        }

        protected override System.Linq.IQueryable<ManifestationKind> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(p => p.Parent).AsTracking();
        }
    }
}
