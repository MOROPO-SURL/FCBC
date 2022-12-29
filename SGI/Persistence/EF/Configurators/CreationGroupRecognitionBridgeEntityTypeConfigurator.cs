using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class CreationGroupRecognitionBridgeEntityTypeConfigurator: SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<CreationGroupRecognitionBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CreationGroupRecognitionBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.GroupId, bridge.RecognitionId });

            entityTypeBuilder.HasOne(bridge => bridge.Group).WithMany(entity => entity.CreationGroupRecognitionBridges).HasForeignKey(bridge => bridge.GroupId);
            entityTypeBuilder.HasOne(bridge => bridge.Recognition).WithMany().HasForeignKey(bridge => bridge.RecognitionId);
        }
    }
}
