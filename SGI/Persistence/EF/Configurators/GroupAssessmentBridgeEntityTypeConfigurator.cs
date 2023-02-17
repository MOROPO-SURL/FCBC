namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class GroupAssessmentBridgeEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<MOROPO.FCBC.SGI.Persistence.Entities.GroupAssessmentBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MOROPO.FCBC.SGI.Persistence.Entities.GroupAssessmentBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.GroupId, bridge.CriticId });

            entityTypeBuilder.HasOne(bridge => bridge.CreationGroup).WithMany(entity => entity.GroupAssessmentBridges).HasForeignKey(bridge => bridge.GroupId);
            entityTypeBuilder.HasOne(bridge => bridge.Critic).WithMany().HasForeignKey(bridge => bridge.CriticId);
        }
    }
}
