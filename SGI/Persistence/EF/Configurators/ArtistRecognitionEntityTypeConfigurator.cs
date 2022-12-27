using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ArtistRecognitionEntityTypeConfigurator: SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<ArtistRecognitionBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ArtistRecognitionBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ArtistId, bridge.RecognitionId });

            entityTypeBuilder.HasOne(bridge => bridge.Artist).WithMany(entity => entity.ArtistRecognitionBridges).HasForeignKey(bridge => bridge.ArtistId);
            entityTypeBuilder.HasOne(bridge => bridge.Recognition).WithMany().HasForeignKey(bridge => bridge.RecognitionId);
        }
    }
}
