using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ServiceLineTranslationEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<ServiceLineTranslation>
    {
        protected override void ApplayRules(EntityTypeBuilder<ServiceLineTranslation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.TranslatableLanguage, entity.ServiceLineId });

            entityTypeBuilder.HasOne(entity => entity.ServiceLine).WithMany(entity => entity.Translations).HasForeignKey(entity => entity.ServiceLineId);
        }
    }
}
