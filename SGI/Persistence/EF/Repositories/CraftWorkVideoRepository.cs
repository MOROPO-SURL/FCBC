using Microsoft.EntityFrameworkCore;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class CraftWorkVideoRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.CraftWorkVideo, System.Guid>
    {
        public CraftWorkVideoRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
