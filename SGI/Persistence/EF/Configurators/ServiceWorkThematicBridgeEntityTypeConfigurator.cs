namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ServiceWorkThematicBridgeEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.ServiceWorkThematicBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ServiceWorkThematicBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.WorkId, bridge.ThematicId });

            entityTypeBuilder.HasOne(bridge => bridge.Work).WithMany(entity => entity.ServiceWorkThematicBridges).HasForeignKey(bridge => bridge.WorkId);
            entityTypeBuilder.HasOne(bridge => bridge.Thematic).WithMany().HasForeignKey(bridge => bridge.ThematicId);
        }
    }
}
