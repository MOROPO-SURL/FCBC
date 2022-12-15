namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class CreationGroupArtisticManifestationBridgeEntityTypeConfigurator: SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<FBCB.SGI.Persistence.Entities.CreationGroupArtisticManifestationBrigde>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FBCB.SGI.Persistence.Entities.CreationGroupArtisticManifestationBrigde> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.CreationGroupId, bridge.ArtisticManifestationId });

            entityTypeBuilder.HasOne(bridge => bridge.CreationGroup).WithMany(bridge => bridge.CreationGroupArtisticManifestationBrigdes).HasForeignKey(bridge => bridge.CreationGroupId);
            entityTypeBuilder.HasOne(bridge => bridge.ArtisticManifestation).WithMany(bridge => bridge.CreationGroupArtisticManifestationBrigdes).HasForeignKey(bridge => bridge.ArtisticManifestationId);
        }
    }
}
