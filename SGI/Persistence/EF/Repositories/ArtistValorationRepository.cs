using Microsoft.EntityFrameworkCore;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ArtistValorationRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ArtistValoration, System.Guid>
    {
        public ArtistValorationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.ArtistValoration> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Critic).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image);
        }
    }
}