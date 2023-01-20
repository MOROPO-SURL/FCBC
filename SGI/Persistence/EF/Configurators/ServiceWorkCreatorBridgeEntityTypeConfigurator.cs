namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ServiceWorkCreatorBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.ServiceWorkCreatorBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ServiceWorkCreatorBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.WorkId, bridge.CreatorId });

            entityTypeBuilder.HasOne(bridge => bridge.Work).WithMany(entity => entity.CreatorBridges).HasForeignKey(bridge => bridge.WorkId);
            entityTypeBuilder.HasOne(bridge => bridge.Creator).WithMany().HasForeignKey(bridge => bridge.CreatorId);
        }
    }
}
