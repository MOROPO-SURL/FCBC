using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class EmployeeRepository: SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Employee, System.Guid>
    {
        public EmployeeRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Employee, object> GetPagingOrderSelector()
        {
            return entity => entity.Identity.Name;
        }

        protected override System.Linq.IQueryable<Employee> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(r => r.AdministrativeRoleBridges).ThenInclude(r => r.Role)
                .Include(e => e.Identity).ThenInclude(p => p.Image)
                .Include(p => p.Identity).ThenInclude(p => p.PersonPostalContactItemBridges).ThenInclude(p => p.ContactItem).ThenInclude(p => p.Region).ThenInclude(p => ((MunicipalityRegion)p).Municipality).ThenInclude(p => p.State)
                .Include(p => p.Identity).ThenInclude(p => p.PersonPhoneContactItemBridges).ThenInclude(p => p.ContactItem)
                .Include(p => p.Identity).ThenInclude(p => p.PersonEmailContactItemBridges).ThenInclude(p => p.ContactItem);
        }
    }
}
