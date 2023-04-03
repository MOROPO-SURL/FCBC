using MOROPO.SAPRESSC.Core.Persistence.Models.Filter;
using System.Linq.Expressions;
using System;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Models.Filter
{
    public class CriticFilterModel : IEntityFilterModel<Critic, System.Guid>
    {
        public string Id { get; set; }
        public Expression<Func<Critic, bool>> GetSelector()
        {
            if (Id == null || Id == string.Empty) return null;

            return critic => critic.Identity.Name.Contains(Id, StringComparison.InvariantCultureIgnoreCase) || critic.Identity.LastName.Contains(Id, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
