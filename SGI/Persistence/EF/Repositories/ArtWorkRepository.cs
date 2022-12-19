using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using System.Linq;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ArtWorkRepository : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ArtWork, System.Guid>
    {
        public ArtWorkRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<ArtWork> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.PriceCurrency)
                .Include(entity => entity.DimensionsUnitMeasurement)
                .Include(entity => entity.ImageBridges).ThenInclude(bridge => bridge.Image)
                .Include(entity => entity.Manifestation)
                .Include(entity => entity.TechniqueBridges).ThenInclude(bridge => bridge.Technique)
                .Include(entity => entity.MaterialBridges).ThenInclude(bridge => bridge.Material);
        }
    }
}
