using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ContractorRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Contractor, System.Guid>
    {
        public ContractorRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Contractor, object> GetPagingOrderSelector()
        {
            return entity => entity.ContractorKind.Name ?? entity.Identity.Name;
        }

        protected override System.Linq.IQueryable<Contractor> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(c => c.ContractorKind)
                .Include(e => e.Identity).ThenInclude(e =>e.Image)
                .Include(e => e.Identity).ThenInclude(e => e.OrganizationPostalContactItemBridges).ThenInclude(e => e.ContactItem).ThenInclude(r => r.Region)
                .Include(e => e.Identity).ThenInclude(e => e.OrganizationEmailContactItemBridges).ThenInclude(e => e.ContactItem)
                .Include(e => e.Identity).ThenInclude(e => e.OrganizationPhoneContactItemBridges).ThenInclude(e => e.ContactItem);
        }
    }
}
