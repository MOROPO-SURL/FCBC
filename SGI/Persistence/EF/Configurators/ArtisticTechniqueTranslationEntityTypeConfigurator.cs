using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ArtisticTechniqueTranslationEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<ArtisticTechniqueTranslation>
    {
        protected override void ApplayRules(EntityTypeBuilder<ArtisticTechniqueTranslation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.TranslatableLanguage, entity.ArtisticTechniqueId });

            entityTypeBuilder.HasOne(entity => entity.ArtisticTechnique).WithMany(entity => entity.Translations).HasForeignKey(entity => entity.ArtisticTechniqueId);
        }
    }
}
