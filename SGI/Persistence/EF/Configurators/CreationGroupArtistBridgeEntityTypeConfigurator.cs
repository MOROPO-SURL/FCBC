namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class CreationGroupArtistBridgeEntityTypeConfigurator : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.CreationGroupArtistBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.CreationGroupArtistBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.CreationGroupId, bridge.ArtistId });

            entityTypeBuilder.HasOne(bridge => bridge.CreationGroup).WithMany(c => c.CreationGroupArtistBridges).HasForeignKey(bridge => bridge.CreationGroupId);
            entityTypeBuilder.HasOne(bridge => bridge.Artist).WithMany(a => a.CreationGroupArtistBridges).HasForeignKey(bridge => bridge.ArtistId);
        }
    }
}
