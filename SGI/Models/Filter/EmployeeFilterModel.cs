using System.Linq.Expressions;
using System;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.Filter;

namespace MOROPO.FCBC.SGI.Models.Filter
{
    public class EmployeeFilterModel: IEntityFilterModel<FBCB.SGI.Persistence.Entities.Employee, System.Guid>
    {
        public string Id { get; set; }
        public Expression<Func<FBCB.SGI.Persistence.Entities.Employee, bool>> GetSelector()
        {
            if (Id == null || Id == string.Empty) return null;

            return critic => critic.PersonIdentity.Name.ToLower().Contains(Id.ToLower());
        }
    }
}
