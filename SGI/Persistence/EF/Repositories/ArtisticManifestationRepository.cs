using MOROPO.FCBC.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ArtisticManifestationRepository : AbstractEntityRepository<ArtisticManifestation, System.Guid>
    {
        public ArtisticManifestationRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<ArtisticManifestation, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
