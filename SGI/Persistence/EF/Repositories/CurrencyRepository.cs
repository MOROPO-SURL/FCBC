using Microsoft.EntityFrameworkCore;
using MOROPO.FBCB.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class CurrencyRepository : AbstractEntityRepository<Currency, int>
    {
        public CurrencyRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Currency, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
