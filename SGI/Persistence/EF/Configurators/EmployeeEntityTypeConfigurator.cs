using MOROPO.FBCB.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class EmployeeEntityTypeConfigurator: SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<FBCB.SGI.Persistence.Entities.Employee>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FBCB.SGI.Persistence.Entities.Employee> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(o => o.PersonIdentity).WithOne().HasForeignKey<Employee>(a => a.PersonIdentityId);
            entityTypeBuilder.HasIndex( o => o.PersonIdentityId);
        }
    }
}
