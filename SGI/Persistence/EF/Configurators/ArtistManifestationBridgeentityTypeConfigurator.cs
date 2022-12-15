namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ArtistManifestationBridgeEntityTypeConfigurator: SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<FBCB.SGI.Persistence.Entities.ArtistManifestationBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FBCB.SGI.Persistence.Entities.ArtistManifestationBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ArtistId, bridge.ArtisticManifestationId});

            entityTypeBuilder.HasOne(bridge => bridge.Artist).WithMany(bridge => bridge.ArtistManifestationBridges).HasForeignKey(bridge => bridge.ArtistId);
            entityTypeBuilder.HasOne(bridge => bridge.ArtisticManifestation).WithMany(bridge => bridge.ArtistManifestationBridges).HasForeignKey(bridge => bridge.ArtisticManifestationId);            
        }
    }
}
