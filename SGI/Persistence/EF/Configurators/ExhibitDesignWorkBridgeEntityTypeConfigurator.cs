namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ExhibitDesignWorkBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.ExhibitDesignWorkBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ExhibitDesignWorkBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ExhibitId, bridge.DesignWorkId });

            entityTypeBuilder.HasOne(bridge => bridge.Exhibit).WithMany(entity => entity.DesignWorkBridges).HasForeignKey(bridge => bridge.ExhibitId);
            entityTypeBuilder.HasOne(bridge => bridge.DesignWork).WithMany().HasForeignKey(bridge => bridge.DesignWorkId);
        }
    }
}
