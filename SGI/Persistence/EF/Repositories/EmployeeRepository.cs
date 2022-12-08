using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class EmployeeRepository: SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<MOROPO.FBCB.SGI.Persistence.Entities.Employee, System.Guid>
    {
        public EmployeeRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<FBCB.SGI.Persistence.Entities.Employee, object> GetPagingOrderSelector()
        {
            return entity => entity.PersonIdentity.Name;
        }

        protected override System.Linq.IQueryable<FBCB.SGI.Persistence.Entities.Employee> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(r => r.EmployeeAdministrativeRoleBridges).ThenInclude(r => r.AdministrativeRole)
                .Include(e => e.PersonIdentity).ThenInclude(p => p.Image)
                .Include(p => p.PersonIdentity).ThenInclude(p => p.PersonPostalContactItemBridges).ThenInclude(p => p.ContactItem).ThenInclude(p => p.Region).ThenInclude(p => ((MunicipalityRegion)p).Municipality).ThenInclude(p => p.State)
                .Include(p => p.PersonIdentity).ThenInclude(p => p.PersonPhoneContactItemBridges).ThenInclude(p => p.ContactItem)
                .Include(p => p.PersonIdentity).ThenInclude(p => p.PersonEmailContactItemBridges).ThenInclude(p => p.ContactItem);
        }
    }
}
