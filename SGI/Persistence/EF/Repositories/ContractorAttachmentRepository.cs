namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ContractorAttachmentRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ContractorAttachment, System.Guid>
    {
        public ContractorAttachmentRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
