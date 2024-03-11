using Microsoft.EntityFrameworkCore;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ArtWorkValorationRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ArtWorkValoration, System.Guid>
    {
        public ArtWorkValorationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.ArtWorkValoration> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Critic).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image);
        }
    }
}
