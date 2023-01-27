namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ArtWorkThematicBridgeEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.ArtWorkThematicBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ArtWorkThematicBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.WorkId, bridge.ThematicId });

            entityTypeBuilder.HasOne(bridge => bridge.Work).WithMany(entity => entity.ArtWorkThematicBridges).HasForeignKey(bridge => bridge.WorkId);
            entityTypeBuilder.HasOne(bridge => bridge.Thematic).WithMany().HasForeignKey(bridge => bridge.ThematicId);
        }
    }
}
