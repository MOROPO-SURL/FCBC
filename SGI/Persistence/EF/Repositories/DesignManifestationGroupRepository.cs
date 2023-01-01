namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class DesignManifestationGroupRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.DesignManifestationGroup, System.Guid>
    {
        public DesignManifestationGroupRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.DesignManifestationGroup, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
