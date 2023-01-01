namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ArtistResolutionLockEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.ArtistResolutionLock>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ArtistResolutionLock> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ArtistId, bridge.ResolutionId });

            entityTypeBuilder.HasOne(bridge => bridge.Artist).WithMany(entity => entity.ResolutionLocks).HasForeignKey(bridge => bridge.ResolutionId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
            entityTypeBuilder.HasOne(bridge => bridge.Resolution).WithMany().HasForeignKey(bridge => bridge.ResolutionId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
