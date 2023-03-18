namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class CraftManifestationGroupRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.CraftManifestationGroup, System.Guid>
    {
        public CraftManifestationGroupRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.CraftManifestationGroup, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
