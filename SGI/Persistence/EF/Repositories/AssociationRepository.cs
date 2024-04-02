using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class AssociationRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Association, System.Guid>
    {
        public AssociationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.Association, object> GetPagingOrderSelector()
        {
            return entity => entity.Identity.Name;
        }

        protected override IQueryable<Entities.Association> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(e => e.Identity).ThenInclude(e => e.Image)
                .Include(e => e.Identity).ThenInclude(e => e.OrganizationPostalContactItemBridges).ThenInclude(p => p.ContactItem).ThenInclude(p => p.Region)
                .Include(e => e.Identity).ThenInclude(e => e.OrganizationEmailContactItemBridges).ThenInclude(e => e.ContactItem)
                .Include(e => e.Identity).ThenInclude(e => e.OrganizationPhoneContactItemBridges).ThenInclude(e => e.ContactItem)
                .Include(e => e.Translations);
        }
    }
}
