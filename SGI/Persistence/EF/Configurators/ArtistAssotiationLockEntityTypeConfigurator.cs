namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ArtistAssotiationLockEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.ArtistAssociationLock>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ArtistAssociationLock> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ArtistId, bridge.AssociationId });

            entityTypeBuilder.HasOne(bridge => bridge.Artist).WithMany(entity => entity.AssociationLocks).HasForeignKey(bridge => bridge.ArtistId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
            entityTypeBuilder.HasOne(bridge => bridge.Association).WithMany().HasForeignKey(bridge => bridge.AssociationId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
