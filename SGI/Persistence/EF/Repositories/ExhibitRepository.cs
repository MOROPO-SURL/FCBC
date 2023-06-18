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
                .Include(entity => entity.ArtWorkBridges).ThenInclude(bridge => bridge.ArtWork).ThenInclude(entity => entity.PriceCurrency)
                .Include(entity => entity.ArtWorkBridges).ThenInclude(bridge => bridge.ArtWork).ThenInclude(entity => entity.DimensionsUnitMeasurement)
                .Include(entity => entity.ArtWorkBridges).ThenInclude(bridge => bridge.ArtWork).ThenInclude(entity => entity.ImageBridges).ThenInclude(bridge => bridge.Image)
                .Include(entity => entity.ArtWorkBridges).ThenInclude(bridge => bridge.ArtWork).ThenInclude(entity => entity.Manifestation)
                .Include(entity => entity.ArtWorkBridges).ThenInclude(bridge => bridge.ArtWork).ThenInclude(entity => entity.TechniqueBridges).ThenInclude(bridge => bridge.Technique)
                .Include(entity => entity.ArtWorkBridges).ThenInclude(bridge => bridge.ArtWork).ThenInclude(entity => entity.MaterialBridges).ThenInclude(bridge => bridge.Material)
                .Include(entity => entity.ArtWorkBridges).ThenInclude(bridge => bridge.ArtWork).ThenInclude(entity => entity.ThematicBridges).ThenInclude(bridge => bridge.Thematic)
                .Include(entity => entity.ArtWorkBridges).ThenInclude(bridge => bridge.ArtWork).ThenInclude(entity => entity.Awards).ThenInclude(award => award.Award)
                .Include(entity => entity.ArtWorkBridges).ThenInclude(bridge => bridge.ArtWork).ThenInclude(entity => entity.Valorations).ThenInclude(award => award.Critic).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image)
                .Include(entity => entity.ArtWorkBridges).ThenInclude(bridge => bridge.ArtWork).ThenInclude(entity => entity.FeatureImage)
                .Include(entity => entity.ArtWorkBridges).ThenInclude(bridge => bridge.ArtWork).ThenInclude(entity => entity.CreatorBridges).ThenInclude(bridge => bridge.Creator).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image)
                .Include(entity => entity.ArtWorkBridges).ThenInclude(bridge => bridge.ArtWork).ThenInclude(entity => entity.CreatorBridges).ThenInclude(bridge => bridge.Creator).ThenInclude(entity => entity.ArtisticManifestationBridges).ThenInclude(bridge => bridge.Manifestation)
                .Include(entity => entity.ArtWorkBridges).ThenInclude(bridge => bridge.ArtWork).ThenInclude(entity => entity.CreatorBridges).ThenInclude(bridge => bridge.Creator).ThenInclude(entity => entity.CraftManifestationBridges).ThenInclude(bridge => bridge.Manifestation)
                .Include(entity => entity.ArtWorkBridges).ThenInclude(bridge => bridge.ArtWork).ThenInclude(entity => entity.CreatorBridges).ThenInclude(bridge => bridge.Creator).ThenInclude(entity => entity.ServiceLineBridges).ThenInclude(bridge => bridge.Line)
                .Include(entity => entity.ArtWorkBridges).ThenInclude(bridge => bridge.ArtWork).ThenInclude(entity => entity.CreationGroupBridges).ThenInclude(bridge => bridge.CreationGroup).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image);
        }
    }
}
