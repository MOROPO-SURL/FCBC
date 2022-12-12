using Microsoft.EntityFrameworkCore;
using MOROPO.FBCB.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ArtisticManifestationRepository : AbstractEntityRepository<ArtisticManifestation, System.Guid>
    {
        public ArtisticManifestationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<ArtisticManifestation, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }

        protected override System.Linq.IQueryable<ArtisticManifestation> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(p => p.Parent);
        }
    }
}
