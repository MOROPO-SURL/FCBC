namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ServiceLineGroupRepository : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ServiceLineGroup, System.Guid>
    {
        public ServiceLineGroupRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.ServiceLineGroup, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
