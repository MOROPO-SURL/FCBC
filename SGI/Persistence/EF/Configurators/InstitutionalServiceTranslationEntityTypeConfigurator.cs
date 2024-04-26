using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class InstitutionalServiceTranslationEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<InstitutionalServiceTranslation>
    {
        protected override void ApplayRules(EntityTypeBuilder<InstitutionalServiceTranslation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.TranslatableLanguage, entity.InstitutionalServiceId });

            entityTypeBuilder.HasOne(entity => entity.InstitutionalService).WithMany(entity => entity.Translations).HasForeignKey(entity => entity.InstitutionalServiceId);



        }
    }
}
