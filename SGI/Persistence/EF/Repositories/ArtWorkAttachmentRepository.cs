namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ArtWorkAttachmentRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ArtWorkAttachment, System.Guid>
    {
        public ArtWorkAttachmentRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
