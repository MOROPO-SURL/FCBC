namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class AssessmentBridgeEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<MOROPO.FCBC.SGI.Persistence.Entities.AssessmentBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MOROPO.FCBC.SGI.Persistence.Entities.AssessmentBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ArtistId, bridge.CriticId });

            entityTypeBuilder.HasOne(bridge => bridge.Artist).WithMany(entity => entity.AssessmentBridges).HasForeignKey(bridge => bridge.ArtistId);
            entityTypeBuilder.HasOne(bridge => bridge.Critic).WithMany().HasForeignKey(bridge => bridge.CriticId);
        }
    }
}
