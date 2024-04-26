using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ServiceTechniqueTranslationEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<ServiceTechniqueTranslation>
    {
        protected override void ApplayRules(EntityTypeBuilder<ServiceTechniqueTranslation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.TranslatableLanguage, entity.ServiceTechniqueId });

            entityTypeBuilder.HasOne(entity => entity.ServiceTechnique).WithMany(entity => entity.Translations).HasForeignKey(entity => entity.ServiceTechniqueId);
        }
    }
}
