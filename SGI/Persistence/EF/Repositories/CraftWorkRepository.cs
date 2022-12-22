using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using System.Linq;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class CraftWorkRepository: SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.CraftWork, System.Guid>
    {

        public CraftWorkRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<CraftWork> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.PriceCurrency)                
                .Include(entity => entity.ImageBridges).ThenInclude(bridge => bridge.Image)
                .Include(entity => entity.Manifestation)
                .Include(entity => entity.TechniqueBridges).ThenInclude(bridge => bridge.Technique);
        }
    }
}
