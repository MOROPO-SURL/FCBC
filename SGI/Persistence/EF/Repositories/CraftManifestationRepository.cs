using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using System.Linq;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class CraftManifestationRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.CraftManifestation, System.Guid>
    {
        public CraftManifestationRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.CraftManifestation, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }

        protected override IQueryable<CraftManifestation> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(p => p.Translations);
        }
    }
}
