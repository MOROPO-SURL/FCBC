namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ExhibitCraftWorkBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.ExhibitCraftWorkBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ExhibitCraftWorkBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ExhibitId, bridge.CraftWorkId });

            entityTypeBuilder.HasOne(bridge => bridge.Exhibit).WithMany(entity => entity.CraftWorkBridges).HasForeignKey(bridge => bridge.ExhibitId);
            entityTypeBuilder.HasOne(bridge => bridge.CraftWork).WithMany().HasForeignKey(bridge => bridge.CraftWorkId);
        }
    }
}
