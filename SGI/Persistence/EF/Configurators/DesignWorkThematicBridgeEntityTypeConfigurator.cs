namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class DesignWorkThematicBridgeEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.DesignWorkThematicBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.DesignWorkThematicBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.WorkId, bridge.ThematicId });

            entityTypeBuilder.HasOne(bridge => bridge.Work).WithMany(entity => entity.ThematicBridges).HasForeignKey(bridge => bridge.WorkId);
            entityTypeBuilder.HasOne(bridge => bridge.Thematic).WithMany().HasForeignKey(bridge => bridge.ThematicId);
        }
    }
}
