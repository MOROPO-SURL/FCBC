using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class EmployeeAdministrativeRoleBridgeEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<EmployeeAdministrativeRoleBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<EmployeeAdministrativeRoleBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey( bridge => new { bridge.RoleId, bridge.EmployeeId});

            entityTypeBuilder.HasOne(bridge => bridge.Role).WithMany().HasForeignKey(bridge => bridge.RoleId);
            entityTypeBuilder.HasOne(bridge => bridge.Employee).WithMany(entity => entity.AdministrativeRoleBridges).HasForeignKey(bridge => bridge.EmployeeId);
        }
    }
}
