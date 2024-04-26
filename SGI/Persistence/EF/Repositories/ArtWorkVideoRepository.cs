namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ArtWorkVideoRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ArtWorkVideo, System.Guid>
    {
        public ArtWorkVideoRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
