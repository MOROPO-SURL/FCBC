namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class CriticEntityTypeConfigurator : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<FBCB.SGI.Persistence.Entities.Critic>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FBCB.SGI.Persistence.Entities.Critic> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(e => e.PersonIdentity).WithMany().HasForeignKey(e => e.PersonIdentityId);
        }
    }
}
