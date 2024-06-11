using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Persistence.Enumerations;
using System.Linq;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class InstitutionalInformationRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<InstitutionalInformation, InstitutionalInformationKind>
    {
        public InstitutionalInformationRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<InstitutionalInformation> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(entity => entity.Translations);
        }
    }
}
