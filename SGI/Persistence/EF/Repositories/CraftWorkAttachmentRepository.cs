using Microsoft.EntityFrameworkCore;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class CraftWorkAttachmentRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.CraftWorkAttachment, System.Guid>
    {
        public CraftWorkAttachmentRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
