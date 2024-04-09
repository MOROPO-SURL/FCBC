using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ArtistRepository: SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Artist, System.Guid>
    {
        public ArtistRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Artist, object> GetPagingOrderSelector()
        {
            return entity => entity.Identity.Name;
        }

        protected override System.Linq.IQueryable<Artist> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(e => e.Identity).ThenInclude(p => p.Image)
                .Include(p => p.Identity).ThenInclude(p => p.PersonPostalContactItemBridges).ThenInclude(p => p.ContactItem).ThenInclude(p => p.Region).ThenInclude(p => ((MunicipalityRegion)p).Municipality).ThenInclude(p => p.State)
                .Include(p => p.Identity).ThenInclude(p => p.PersonPhoneContactItemBridges).ThenInclude(p => p.ContactItem)
                .Include(p => p.Identity).ThenInclude(p => p.PersonEmailContactItemBridges).ThenInclude(p => p.ContactItem)
                .Include(p => p.Identity).ThenInclude(p => p.PersonSocialContactItemBridges).ThenInclude(p => p.ContactItem)
                .Include(p => p.ArtisticManifestationBridges).ThenInclude(p => p.Manifestation)
                .Include(p => p.CraftManifestationBridges).ThenInclude(b => b.Manifestation)
                .Include(p => p.DesignManifestationBridges).ThenInclude(b => b.Manifestation)
                .Include(p => p.ServiceLineBridges).ThenInclude(b => b.Line)
                .Include(p => p.CreationGroupArtistBridges).ThenInclude(p =>  p.CreationGroup)
                .Include(p => p.ArtistAwards).ThenInclude(p => p.Award).ThenInclude(p => p.Country)
                .Include(p => p.ArtistRecognitionBridges).ThenInclude(p => p.Recognition)
                .Include(p => p.ArtistDistinctionBridges).ThenInclude(p => p.Distinction)
                .Include(p => p.Attachments)
                .Include(p => p.Translations);
        }
    }
}
