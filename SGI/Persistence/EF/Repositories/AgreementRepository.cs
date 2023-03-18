namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class AgreementRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Agreement, System.Guid>
    {
        public AgreementRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.Agreement, object> GetPagingOrderSelector()
        {
            return entity => entity.ReleaseAt;
        }

        public override bool IsPagingOrderDescending()
        {
            return true;
        }
    }
}
