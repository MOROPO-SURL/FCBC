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
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtisticManifestationViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtisticManifestationViewer.ToRoleEntity());
            }
            #region ArtisticManifestation
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtisticManifestationCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtisticManifestationCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtisticManifestationEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtisticManifestationEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtisticManifestationRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtisticManifestationRemover.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.AwardViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.AwardViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.AwardCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.AwardCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.AwardEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.AwardEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.AwardRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.AwardRemover.ToRoleEntity());
            }
            #endregion

            #region Distinction
            if (!roleDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Roles.DistinctionViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DistinctionViewer.ToRoleEntity());
            }
            if(!roleDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Roles.DistinctionCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DistinctionCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Roles.DistinctionEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DistinctionEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Roles.DistinctionRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DistinctionRemover.ToRoleEntity());
            }
            #endregion

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