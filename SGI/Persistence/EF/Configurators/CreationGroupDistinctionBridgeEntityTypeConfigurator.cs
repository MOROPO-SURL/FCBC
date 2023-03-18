using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class CreationGroupDistinctionBridgeEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<CreationGroupDistinctionBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CreationGroupDistinctionBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.GroupId, bridge.DistinctionId });

            entityTypeBuilder.HasOne(bridge => bridge.Group).WithMany(entity => entity.CreationGroupDistinctionBridges).HasForeignKey(bridge => bridge.GroupId);
            entityTypeBuilder.HasOne(bridge => bridge.Distinction).WithMany().HasForeignKey(bridge => bridge.DistinctionId);
        }
    }
}
