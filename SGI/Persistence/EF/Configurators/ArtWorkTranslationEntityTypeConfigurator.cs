using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ArtWorkTranslationEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<ArtWorkTranslation>
    {
        protected override void ApplayRules(EntityTypeBuilder<ArtWorkTranslation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.TranslatableLanguage, entity.ArtWorkId});

            entityTypeBuilder.HasOne(entity => entity.ArtWork).WithMany(entity => entity.Translations).HasForeignKey(entity => entity.ArtWorkId);

            

        }
    }
}
