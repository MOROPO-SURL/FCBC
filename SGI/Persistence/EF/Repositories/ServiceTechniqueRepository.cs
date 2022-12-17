namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ServiceTechniqueRepository : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ServiceTechnique, System.Guid>
    {
        public ServiceTechniqueRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.ServiceTechnique, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
