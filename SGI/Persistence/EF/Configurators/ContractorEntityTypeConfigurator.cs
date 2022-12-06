using MOROPO.FBCB.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ContractorEntityTypeConfigurator: SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<FBCB.SGI.Persistence.Entities.Contractor>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FBCB.SGI.Persistence.Entities.Contractor> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(o => o.OrganizationIdentity).WithOne().HasForeignKey<Contractor>(a => a.OrganizationIdentityId);
            entityTypeBuilder.HasOne(c => c.ContractorKind).WithMany().HasForeignKey(a => a.ContractorKindId);
            
        }
    }
}
