namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ArtisticManifestationGroupRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ArtisticManifestationGroup, System.Guid>
    {
        public ArtisticManifestationGroupRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.ArtisticManifestationGroup, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
