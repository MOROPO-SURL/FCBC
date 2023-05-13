namespace MOROPO.FCBC.SGI.API.Common.Models.Filter
{
    public class ExhibitFilterApiModel : SAPRESSC.Core.Persistence.Models.Filter.IEntityFilterModel<Persistence.Entities.Exhibit, System.Guid>
    {
        public System.Guid ExposureId { get; set; }

        public System.Linq.Expressions.Expression<System.Func<Persistence.Entities.Exhibit, bool>> GetSelector()
        {
            return entity => entity.ExposureId == ExposureId;
        }
    }
}
