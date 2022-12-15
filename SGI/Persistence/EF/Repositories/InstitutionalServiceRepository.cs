
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class InstitutionalServiceRepository : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<InstitutionalService, System.Guid>
    {
        public InstitutionalServiceRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<InstitutionalService, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
