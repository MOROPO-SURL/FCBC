using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using System.Linq;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ServiceTechniqueRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ServiceTechnique, System.Guid>
    {
        public ServiceTechniqueRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.ServiceTechnique, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }

        protected override IQueryable<ServiceTechnique> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(p => p.Translations);
        }
    }
}
