using Microsoft.EntityFrameworkCore;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class CraftWorkValorationRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.CraftWorkValoration, System.Guid>
    {
        public CraftWorkValorationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.CraftWorkValoration> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Critic).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image);
        }
    }
}
