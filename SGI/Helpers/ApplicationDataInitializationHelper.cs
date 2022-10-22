using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.SAPRESSC.Core.Security.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Web.SecurityLocal.Extensions;

namespace MOROPO.FCBC.SGI.Helpers
{
    public sealed class ApplicationDataInitializationHelper : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Helpers.Facade.IDataInitializationHelper
    {
        public void InitialiseData(DbContext dbContext)
        {
            var roleDbSet = dbContext.Set<Role>();

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.TraceViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.TraceViewer.ToRoleEntity());
            }

            if (dbContext is SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Context.ITraceableDbContext)
            {
                (dbContext as SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Context.ITraceableDbContext).SaveChanges("system");
            }
            else
            {
                dbContext.SaveChanges();
            }
        }
    }
}