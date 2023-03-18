namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class CreationGroupAttachmentRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.CreationGroupAttachment, System.Guid>
    {
        public CreationGroupAttachmentRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
