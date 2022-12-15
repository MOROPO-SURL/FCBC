using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ContractorEntityTypeConfigurator: SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Contractor>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Contractor> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(o => o.Identity).WithOne().HasForeignKey<Contractor>(a => a.IdentityId);
            entityTypeBuilder.HasOne(c => c.ContractorKind).WithMany().HasForeignKey(a => a.ContractorKindId);
            
        }
    }
}
