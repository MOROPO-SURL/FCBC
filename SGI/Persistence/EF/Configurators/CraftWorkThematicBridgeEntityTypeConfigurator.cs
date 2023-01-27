namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class CraftWorkThematicBridgeEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.CraftWorkThematicBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.CraftWorkThematicBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.WorkId, bridge.ThematicId });

            entityTypeBuilder.HasOne(bridge => bridge.Work).WithMany(entity => entity.CraftWorkThematicBridges).HasForeignKey(bridge => bridge.WorkId);
            entityTypeBuilder.HasOne(bridge => bridge.Thematic).WithMany().HasForeignKey(bridge => bridge.ThematicId);
        }
    }
}
