namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ResolutionRepository: SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Resolution, System.Guid>
    {
        public ResolutionRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.Resolution, object> GetPagingOrderSelector()
        {
            return entity => entity.ReleaseAt;
        }
    }
}
