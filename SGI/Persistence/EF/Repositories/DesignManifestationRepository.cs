namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class DesignManifestationRepository : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.DesignManifestation, System.Guid>
    {
        public DesignManifestationRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.DesignManifestation, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
