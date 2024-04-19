using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using System.Linq;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ArtisticManifestationRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ArtisticManifestation, System.Guid>
    {
        public ArtisticManifestationRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.ArtisticManifestation, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }

        protected override IQueryable<ArtisticManifestation> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(p => p.Translations);
        }
    }
}