namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ArtWorkMaterialBridgeEntityTypeConfigurator : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.ArtWorkMaterialBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ArtWorkMaterialBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.WorkId, bridge.MaterialId });

            entityTypeBuilder.HasOne(bridge => bridge.Work).WithMany(entity => entity.MaterialBridges).HasForeignKey(bridge => bridge.WorkId);
            entityTypeBuilder.HasOne(bridge => bridge.Material).WithMany().HasForeignKey(bridge => bridge.MaterialId);
        }
    }
}
