namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ServiceLineRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ServiceLine, System.Guid>
    {
        public ServiceLineRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.ServiceLine, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
