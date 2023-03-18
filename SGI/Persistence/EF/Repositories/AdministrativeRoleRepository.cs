using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class AdministrativeRoleRepository: SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<AdministrativeRole, System.Guid>
    {
        public AdministrativeRoleRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<AdministrativeRole, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
