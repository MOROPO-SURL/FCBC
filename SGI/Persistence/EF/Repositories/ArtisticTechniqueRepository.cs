using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using System.Linq;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ArtisticTechniqueRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ArtisticTechnique, System.Guid>
    {
        public ArtisticTechniqueRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.ArtisticTechnique, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }

        protected override IQueryable<ArtisticTechnique> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(p => p.Translations);
        }
    }
}
