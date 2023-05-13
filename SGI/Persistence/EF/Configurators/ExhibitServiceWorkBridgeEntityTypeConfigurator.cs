namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ExhibitServiceWorkBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.ExhibitServiceWorkBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ExhibitServiceWorkBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ExhibitId, bridge.ServiceWorkId });

            entityTypeBuilder.HasOne(bridge => bridge.Exhibit).WithMany(entity => entity.ServiceWorkBridges).HasForeignKey(bridge => bridge.ExhibitId);
            entityTypeBuilder.HasOne(bridge => bridge.ServiceWork).WithMany().HasForeignKey(bridge => bridge.ServiceWorkId);
        }
    }
}
