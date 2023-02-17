namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ArtistAssessmentBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<MOROPO.FCBC.SGI.Persistence.Entities.ArtistAssessmentBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MOROPO.FCBC.SGI.Persistence.Entities.ArtistAssessmentBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ArtistId, bridge.CriticId });

            entityTypeBuilder.HasOne(bridge => bridge.Artist).WithMany(entity => entity.ArtistAssessmentBridges).HasForeignKey(bridge => bridge.ArtistId);
            entityTypeBuilder.HasOne(bridge => bridge.Critic).WithMany().HasForeignKey(bridge => bridge.CriticId);
        }
    }
}
