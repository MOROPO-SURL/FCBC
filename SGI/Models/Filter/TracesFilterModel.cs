using MOROPO.FCBC.SGI.Models.View;
using MOROPO.SAPRESSC.Core.Persistence.EF.Entities;
using MOROPO.SAPRESSC.Core.Persistence.Models.Filter;
using System;
using System.Linq.Expressions;

namespace MOROPO.FCBC.SGI.Models.Filter
{
    public class TracesFilterModel : IEntityFilterModel<Trace, long>
    {
        public string Id { get; set; }
        public Expression<Func<Trace, bool>> GetSelector()
        {
            if (Id == null || Id == string.Empty) return null;

            return user => user.Author.Contains(Id);
        }
    }
}
