
namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ServiceRepository : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<FBCB.SGI.Persistence.Entities.Service, System.Guid>
    {
        public ServiceRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<FBCB.SGI.Persistence.Entities.Service, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
