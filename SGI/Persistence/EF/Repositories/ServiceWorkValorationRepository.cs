using Microsoft.EntityFrameworkCore;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ServiceWorkValorationRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ServiceWorkValoration, System.Guid>
    {
        public ServiceWorkValorationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.ServiceWorkValoration> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Critic).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image);
        }
    }
}
