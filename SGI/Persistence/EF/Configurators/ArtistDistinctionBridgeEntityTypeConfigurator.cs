using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ArtistDistinctionBridgeEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<ArtistDistinctionBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ArtistDistinctionBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ArtistId, bridge.DistinctionId });

            entityTypeBuilder.HasOne(bridge => bridge.Artist).WithMany(entity => entity.ArtistDistinctionBridges).HasForeignKey(bridge => bridge.ArtistId);
            entityTypeBuilder.HasOne(bridge => bridge.Distinction).WithMany().HasForeignKey(bridge => bridge.DistinctionId);
        }
    }
}
