namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class DesignTechniqueRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.DesignTechnique, System.Guid>
    {
        public DesignTechniqueRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.DesignTechnique, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
