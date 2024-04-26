using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ServiceWorkTranslationEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<ServiceWorkTranslation>
    {
        protected override void ApplayRules(EntityTypeBuilder<ServiceWorkTranslation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.TranslatableLanguage, entity.ServiceWorkId });

            entityTypeBuilder.HasOne(entity => entity.ServiceWork).WithMany(entity => entity.Translations).HasForeignKey(entity => entity.ServiceWorkId);



        }
    }
}
