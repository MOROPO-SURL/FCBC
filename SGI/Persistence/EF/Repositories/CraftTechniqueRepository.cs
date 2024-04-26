using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using System.Linq;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class CraftTechniqueRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.CraftTechnique, System.Guid>
    {
        public CraftTechniqueRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.CraftTechnique, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }

        protected override IQueryable<CraftTechnique> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(p => p.Translations);
        }
    }
}
