using MOROPO.FBCB.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class EmployeeAdministrativeRoleBridgeEntityTypeConfigurator: SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<FBCB.SGI.Persistence.Entities.EmployeeAdministrativeRoleBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FBCB.SGI.Persistence.Entities.EmployeeAdministrativeRoleBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey( bridge => new { bridge.AdministrativeRoleId, bridge.EmployeeId});

            entityTypeBuilder.HasOne(bridge => bridge.AdministrativeRole).WithMany(admRol => admRol.EmployeeAdministrativeRoleBridges).HasForeignKey(bridge => bridge.AdministrativeRoleId);
            entityTypeBuilder.HasOne(bridge => bridge.Employee).WithMany(emp => emp.EmployeeAdministrativeRoleBridges).HasForeignKey(bridge => bridge.EmployeeId);
        }
    }
}
