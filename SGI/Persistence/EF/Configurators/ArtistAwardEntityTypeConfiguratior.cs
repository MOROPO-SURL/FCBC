using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ArtistAwardEntityTypeConfiguratior: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<ArtistAwardBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ArtistAwardBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ArtistId, bridge.AwardId });

            entityTypeBuilder.HasOne(bridge => bridge.Artist).WithMany(entity => entity.ArtistAwardBridges).HasForeignKey(bridge => bridge.ArtistId);
            entityTypeBuilder.HasOne(bridge => bridge.Award).WithMany().HasForeignKey(bridge => bridge.AwardId);
        }
    }
}
