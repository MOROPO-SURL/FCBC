using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ExposureTranslationEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<ExposureTranslation>
    {
        protected override void ApplayRules(EntityTypeBuilder<ExposureTranslation> entityTypeBuilder)
    {
        base.ApplayRules(entityTypeBuilder);

        entityTypeBuilder.HasKey(entity => new { entity.TranslatableLanguage, entity.ExposureId });

        entityTypeBuilder.HasOne(entity => entity.Exposure).WithMany(entity => entity.Translations).HasForeignKey(entity => entity.ExposureId);



    }
}
}
