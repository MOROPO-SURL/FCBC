namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class AssociationEntityTypeConfigurator : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<FBCB.SGI.Persistence.Entities.Association>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FBCB.SGI.Persistence.Entities.Association> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(o => o.OrganizationIdentity).WithOne().HasForeignKey<FBCB.SGI.Persistence.Entities.Association>(a => a.OrganizationIdentityId);
        }
    }
}
