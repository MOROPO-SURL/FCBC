using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class CreationGroupDesignManifestationBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<CreationGroupDesignManifestationBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CreationGroupDesignManifestationBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.GroupId, bridge.ManifestationId });

            entityTypeBuilder.HasOne(bridge => bridge.Group).WithMany(entity => entity.DesignManifestationBridges).HasForeignKey(bridge => bridge.GroupId);
            entityTypeBuilder.HasOne(bridge => bridge.Manifestation).WithMany().HasForeignKey(bridge => bridge.ManifestationId);
        }
    }
}
