namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ServiceWorkImageBridgeEntityTypeConfigurator : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.ServiceWorkImageBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ServiceWorkImageBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.WorkId, bridge.ImageId });

            entityTypeBuilder.HasOne(bridge => bridge.Work).WithMany(entity => entity.ImageBridges).HasForeignKey(bridge => bridge.WorkId);
            entityTypeBuilder.HasOne(bridge => bridge.Image).WithMany().HasForeignKey(bridge => bridge.ImageId);
        }
    }
}
