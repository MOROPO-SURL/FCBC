namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class CraftTechniqueRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.CraftTechnique, System.Guid>
    {
        public CraftTechniqueRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.CraftTechnique, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
