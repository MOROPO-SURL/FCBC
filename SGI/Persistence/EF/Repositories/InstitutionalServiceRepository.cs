using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using System.Linq;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class InstitutionalServiceRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<InstitutionalService, System.Guid>
    {
        public InstitutionalServiceRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<InstitutionalService, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }

        protected override IQueryable<InstitutionalService> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
               .Include(entity => entity.Translations);
        }
    }
}
