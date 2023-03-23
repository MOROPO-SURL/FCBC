namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ExhibitArtWorkBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.ExhibitArtWorkBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ExhibitArtWorkBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ExhibitId, bridge.ArtWorkId });

            entityTypeBuilder.HasOne(bridge => bridge.Exhibit).WithMany(entity => entity.ArtWorkBridges).HasForeignKey(bridge => bridge.ExhibitId);
            entityTypeBuilder.HasOne(bridge => bridge.ArtWork).WithMany().HasForeignKey(bridge => bridge.ArtWorkId);
        }
    }
}
