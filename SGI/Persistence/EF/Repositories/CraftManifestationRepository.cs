namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class CraftManifestationRepository : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.CraftManifestation, System.Guid>
    {
        public CraftManifestationRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.CraftManifestation, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
