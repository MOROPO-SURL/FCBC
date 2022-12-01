
namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class CreationGroupEntityTypeConfigurator : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<FBCB.SGI.Persistence.Entities.CreationGroup>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FBCB.SGI.Persistence.Entities.CreationGroup> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(o => o.OrganizationIdentity).WithOne().HasForeignKey<FBCB.SGI.Persistence.Entities.CreationGroup>(a => a.OrganizationIdentityId);
        }
    }
}
