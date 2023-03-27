using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ExposureRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Exposure, System.Guid>
    {
        public ExposureRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Exposure, object> GetPagingOrderSelector()
        {
            return entity => entity.CreatedAt;
        }

        public override bool IsPagingOrderDescending()
        {
            return true;
        }
    }
}
