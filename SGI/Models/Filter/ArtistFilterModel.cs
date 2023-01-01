using System.Linq.Expressions;
using System;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Models.Filter
{
    public class ArtistFilterModel: SAPRESSC.Core.Persistence.Models.Filter.IEntityFilterModel<Artist, System.Guid>
    {
        public string Id { get; set; }
        public Expression<Func<Artist, bool>> GetSelector()
        {
            if (Id == null || Id == string.Empty) return null;

            return critic => critic.Identity.Name.ToLower().Contains(Id.ToLower());
        }
    }
}
