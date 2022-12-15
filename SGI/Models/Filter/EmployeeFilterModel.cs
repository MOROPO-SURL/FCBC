using System.Linq.Expressions;
using System;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.Filter;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Models.Filter
{
    public class EmployeeFilterModel: IEntityFilterModel<Employee, System.Guid>
    {
        public string Id { get; set; }
        public Expression<Func<Employee, bool>> GetSelector()
        {
            if (Id == null || Id == string.Empty) return null;

            return critic => critic.Identity.Name.ToLower().Contains(Id.ToLower());
        }
    }
}
