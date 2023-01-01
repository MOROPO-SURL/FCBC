using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class UnitMeasurementRepository: SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<UnitMeasurement, System.Guid>
    {
        public UnitMeasurementRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<UnitMeasurement, object> GetPagingOrderSelector()
        {
            return entity => entity.Name ?? entity.Symbol;
        }
    }
}
