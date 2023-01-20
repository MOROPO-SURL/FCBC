using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using System.Linq;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ServiceWorkRepository: SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ServiceWork, System.Guid>
    {
        public ServiceWorkRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<ServiceWork> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.PriceCurrency)
                .Include(entity => entity.ImageBridges).ThenInclude(bridge => bridge.Image)
                .Include(entity => entity.Line)
                .Include(entity => entity.TechniqueBridges).ThenInclude(bridge => bridge.Technique)
                .Include(entity => entity.MaterialBridges).ThenInclude(bridge => bridge.Material)
                .Include(entity => entity.Attachments)
                .Include(entity => entity.Awards).ThenInclude(award => award.Award).ThenInclude(c => c.Country)
                .Include(entity => entity.CreatorBridges).ThenInclude(bridge => bridge.Creator).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image)
                .Include(entity => entity.CreationGroupBridges).ThenInclude(bridge => bridge.CreationGroup).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image);
        }
    }
}
