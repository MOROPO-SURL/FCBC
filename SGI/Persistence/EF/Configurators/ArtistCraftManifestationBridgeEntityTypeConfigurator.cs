using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ArtistCraftManifestationBridgeEntityTypeConfigurator : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<ArtistCraftManifestationBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ArtistCraftManifestationBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ArtistId, bridge.ManifestationId });

            entityTypeBuilder.HasOne(bridge => bridge.Artist).WithMany(entity => entity.CraftManifestationBridges).HasForeignKey(bridge => bridge.ArtistId);
            entityTypeBuilder.HasOne(bridge => bridge.Manifestation).WithMany().HasForeignKey(bridge => bridge.ManifestationId);
        }
    }
}