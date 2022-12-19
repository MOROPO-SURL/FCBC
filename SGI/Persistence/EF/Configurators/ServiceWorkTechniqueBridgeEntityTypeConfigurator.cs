namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ServiceWorkTechniqueBridgeEntityTypeConfigurator : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.ServiceWorkTechniqueBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ServiceWorkTechniqueBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.WorkId, bridge.TechniqueId });

            entityTypeBuilder.HasOne(bridge => bridge.Work).WithMany(entity => entity.TechniqueBridges).HasForeignKey(bridge => bridge.WorkId);
            entityTypeBuilder.HasOne(bridge => bridge.Technique).WithMany().HasForeignKey(bridge => bridge.TechniqueId);
        }
    }
}
