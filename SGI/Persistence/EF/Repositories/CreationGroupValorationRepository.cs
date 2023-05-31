using Microsoft.EntityFrameworkCore;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class CreationGroupValorationRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.CreationGroupValoration, System.Guid>
    {
        public CreationGroupValorationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.CreationGroupValoration> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Critic).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image);
        }
    }
}
