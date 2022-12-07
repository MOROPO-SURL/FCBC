using Microsoft.EntityFrameworkCore;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ContractorRepository : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<MOROPO.FBCB.SGI.Persistence.Entities.Contractor, System.Guid>
    {
        public ContractorRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<FBCB.SGI.Persistence.Entities.Contractor, object> GetPagingOrderSelector()
        {
            return entity => entity.ContractorKind.Name ?? entity.OrganizationIdentity.Name;
        }

        protected override System.Linq.IQueryable<FBCB.SGI.Persistence.Entities.Contractor> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(c => c.ContractorKind)
                .Include(e => e.OrganizationIdentity).ThenInclude(e =>e.Image)
                .Include(e => e.OrganizationIdentity).ThenInclude(e => e.OrganizationPostalContactItemBridges).ThenInclude(e => e.ContactItem).ThenInclude(r => r.Region)
                .Include(e => e.OrganizationIdentity).ThenInclude(e => e.OrganizationEmailContactItemBridges).ThenInclude(e => e.ContactItem)
                .Include(e => e.OrganizationIdentity).ThenInclude(e => e.OrganizationPhoneContactItemBridges).ThenInclude(e => e.ContactItem);
        }
    }
}
