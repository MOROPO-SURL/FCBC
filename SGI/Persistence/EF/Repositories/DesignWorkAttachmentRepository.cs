using Microsoft.EntityFrameworkCore;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class DesignWorkAttachmentRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.DesignWorkAttachment, System.Guid>
    {
        public DesignWorkAttachmentRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
