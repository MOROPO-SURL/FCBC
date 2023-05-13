namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class GalleryEmployeeBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.GalleryEmployeeBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.GalleryEmployeeBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.GalleryId, bridge.EmployeeId });

            entityTypeBuilder.HasOne(bridge => bridge.Gallery).WithMany(entity => entity.EmployeeBridges).HasForeignKey(bridge => bridge.GalleryId);
            entityTypeBuilder.HasOne(bridge => bridge.Employee).WithMany().HasForeignKey(bridge => bridge.EmployeeId);
        }
    }
}
