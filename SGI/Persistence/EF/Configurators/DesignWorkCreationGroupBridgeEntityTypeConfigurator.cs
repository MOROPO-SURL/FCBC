namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class DesignWorkCreationGroupBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.DesignWorkCreationGroupBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.DesignWorkCreationGroupBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.WorkId, bridge.CreationGroupId });

            entityTypeBuilder.HasOne(bridge => bridge.Work).WithMany(entity => entity.CreationGroupBridges).HasForeignKey(bridge => bridge.WorkId);
            entityTypeBuilder.HasOne(bridge => bridge.CreationGroup).WithMany().HasForeignKey(bridge => bridge.CreationGroupId);
        }
    }
}
