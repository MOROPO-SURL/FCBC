using Microsoft.EntityFrameworkCore;
using MOROPO.FBCB.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ResolutionRepository: AbstractEntityRepository<Resolution, System.Guid>
    {
        public ResolutionRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Resolution, object> GetPagingOrderSelector()
        {
            return entity => entity.NumberResolution ?? entity.Title ?? entity.Description;
        }
    }
}
