using System.Linq.Expressions;
using System;

namespace MOROPO.FCBC.SGI.Models.Filter
{
    public class ArtistFilterModel: SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.Filter.IEntityFilterModel<FBCB.SGI.Persistence.Entities.Artist, System.Guid>
    {
        public string Id { get; set; }
        public Expression<Func<FBCB.SGI.Persistence.Entities.Artist, bool>> GetSelector()
        {
            if (Id == null || Id == string.Empty) return null;

            return critic => critic.PersonIdentity.Name.ToLower().Contains(Id.ToLower());
        }
    }
}
