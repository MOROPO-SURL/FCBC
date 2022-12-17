namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class CraftWorkImageBridgeEntityTypeConfigurator : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.CraftWorkImageBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.CraftWorkImageBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.WorkId, bridge.ImageId });

            entityTypeBuilder.HasOne(bridge => bridge.Work).WithMany(entity => entity.ImageBridges).HasForeignKey(bridge => bridge.WorkId);
            entityTypeBuilder.HasOne(bridge => bridge.Image).WithMany().HasForeignKey(bridge => bridge.ImageId);
        }
    }
}
