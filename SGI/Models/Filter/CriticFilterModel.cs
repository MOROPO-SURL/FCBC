using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.Filter;
using System.Linq.Expressions;
using System;

namespace MOROPO.FCBC.SGI.Models.Filter
{
    public class CriticFilterModel : IEntityFilterModel<FBCB.SGI.Persistence.Entities.Critic, System.Guid>
    {
        public string Id { get; set; }
        public Expression<Func<FBCB.SGI.Persistence.Entities.Critic, bool>> GetSelector()
        {
            if (Id == null || Id == string.Empty) return null;

            return critic => critic.PersonIdentity.Name.ToLower().Contains(Id.ToLower());
        }
    }
}
