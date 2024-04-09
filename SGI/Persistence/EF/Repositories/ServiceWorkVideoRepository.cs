using Microsoft.EntityFrameworkCore;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ServiceWorkVideoRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ServiceWorkVideo, System.Guid>
    {
        public ServiceWorkVideoRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
