using Microsoft.EntityFrameworkCore;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class AdministrativeRoleRepository: SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<MOROPO.FBCB.SGI.Persistence.Entities.AdministrativeRole, System.Guid>
    {
        public AdministrativeRoleRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<MOROPO.FBCB.SGI.Persistence.Entities.AdministrativeRole, object> GetPagingOrderSelector()
        {
            return entity => entity.Name ?? entity.Description;

        }
    }
}
