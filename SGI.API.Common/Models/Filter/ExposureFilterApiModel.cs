namespace MOROPO.FCBC.SGI.API.Common.Models.Filter
{
    public class ExposureFilterApiModel : SAPRESSC.Core.Persistence.Models.Filter.IEntityFilterModel<Persistence.Entities.Exposure, System.Guid>
    {
        public Persistence.Enumerations.GalleryId GalleryId { get; set; }

        public System.Linq.Expressions.Expression<System.Func<Persistence.Entities.Exposure, bool>> GetSelector()
        {
            return entity => entity.GalleryId == GalleryId;
        }
    }
}
