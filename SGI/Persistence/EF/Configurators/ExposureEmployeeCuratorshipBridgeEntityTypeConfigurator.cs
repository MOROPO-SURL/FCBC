namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ExposureEmployeeCuratorshipBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.ExposureEmployeeCuratorshipBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ExposureEmployeeCuratorshipBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ExposureId, bridge.EmployeeId });

            entityTypeBuilder.HasOne(bridge => bridge.Exposure).WithMany(entity => entity.EmployeeCuratorshipBridges).HasForeignKey(bridge => bridge.ExposureId);
            entityTypeBuilder.HasOne(bridge => bridge.Employee).WithMany().HasForeignKey(bridge => bridge.EmployeeId);
        }
    }
}
