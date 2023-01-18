namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class CraftWorkCreationGroupBridgeEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.CraftWorkCreationGroupBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.CraftWorkCreationGroupBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.WorkId, bridge.CreationGroupId });

            entityTypeBuilder.HasOne(bridge => bridge.Work).WithMany(entity => entity.CreationGroupBridges).HasForeignKey(bridge => bridge.WorkId);
            entityTypeBuilder.HasOne(bridge => bridge.CreationGroup).WithMany().HasForeignKey(bridge => bridge.CreationGroupId);
        }
    }
}
