
namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ContractorKindRepository : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<FBCB.SGI.Persistence.Entities.ContractorKind, int>
    {
        public ContractorKindRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<FBCB.SGI.Persistence.Entities.ContractorKind, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
