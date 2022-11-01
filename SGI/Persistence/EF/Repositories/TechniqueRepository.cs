namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class TechniqueRepository : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<FBCB.SGI.Persistence.Entities.Technique, System.Guid>
    {
        public TechniqueRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<FBCB.SGI.Persistence.Entities.Technique, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
