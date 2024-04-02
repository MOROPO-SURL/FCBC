using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using System.Linq;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class GalleryRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Gallery, Enumerations.GalleryId>
    {
        public GalleryRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Gallery> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Identity).ThenInclude(entity => entity.Image)
                .Include(entity => entity.Identity).ThenInclude(entity => entity.OrganizationEmailContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                .Include(entity => entity.Identity).ThenInclude(entity => entity.OrganizationPhoneContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                .Include(entity => entity.Identity).ThenInclude(entity => entity.OrganizationSocialContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                .Include(entity => entity.Identity).ThenInclude(entity => entity.OrganizationWebSiteContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                .Include(entity => entity.EmployeeBridges).ThenInclude(bridge => bridge.Employee).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image)
                .Include(entity => entity.Translations);
        }
    }
}
