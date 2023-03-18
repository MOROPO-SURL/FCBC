namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ArtistAttachmentRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ArtistAttachment, System.Guid>
    {
        public ArtistAttachmentRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
