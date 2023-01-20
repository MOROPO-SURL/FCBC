using Microsoft.EntityFrameworkCore;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ServiceWorkAttachmentRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ServiceWorkAttachment, System.Guid>
    {
        public ServiceWorkAttachmentRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
