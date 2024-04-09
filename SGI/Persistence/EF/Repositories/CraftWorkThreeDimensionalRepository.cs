using Microsoft.EntityFrameworkCore;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class CraftWorkThreeDimensionalRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.CraftWorkThreeDimensional, System.Guid>
    {
        public CraftWorkThreeDimensionalRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
