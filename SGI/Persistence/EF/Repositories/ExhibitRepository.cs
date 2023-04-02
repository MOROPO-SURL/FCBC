using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using System.Linq;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ExhibitRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Exhibit, System.Guid>
    {
        public ExhibitRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Exhibit> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.ArtWorkBridges).ThenInclude(bridge => bridge.ArtWork).ThenInclude(entity => entity.FeatureImage);
        }
    }
}
