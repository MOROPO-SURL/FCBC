namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class CraftWorkAssessmentBridgeEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<MOROPO.FCBC.SGI.Persistence.Entities.CraftWorkAssessmentBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MOROPO.FCBC.SGI.Persistence.Entities.CraftWorkAssessmentBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.WorkId, bridge.CriticId });

            entityTypeBuilder.HasOne(bridge => bridge.Work).WithMany(entity => entity.WorkAssessmentBridges).HasForeignKey(bridge => bridge.WorkId);
            entityTypeBuilder.HasOne(bridge => bridge.Critic).WithMany().HasForeignKey(bridge => bridge.CriticId);
        }
    }
}
