namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ArtisticTechniqueRepository : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ArtisticTechnique, System.Guid>
    {
        public ArtisticTechniqueRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.ArtisticTechnique, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
