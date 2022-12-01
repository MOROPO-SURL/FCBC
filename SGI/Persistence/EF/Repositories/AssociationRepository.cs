using Microsoft.EntityFrameworkCore;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class AssociationRepository : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<FBCB.SGI.Persistence.Entities.Association, System.Guid>
    {
        public AssociationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<FBCB.SGI.Persistence.Entities.Association, object> GetPagingOrderSelector()
        {
            return entity => entity.OrganizationIdentity.Name;
        }

        protected override System.Linq.IQueryable<FBCB.SGI.Persistence.Entities.Association> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(e => e.OrganizationIdentity).ThenInclude(e => e.Image);
        }
    }
}
