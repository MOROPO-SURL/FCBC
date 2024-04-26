using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class GalleryTranslationEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<GalleryTranslation>
    {
        protected override void ApplayRules(EntityTypeBuilder<GalleryTranslation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.TranslatableLanguage, entity.GalleryId });

            entityTypeBuilder.HasOne(entity => entity.Gallery).WithMany(entity => entity.Translations).HasForeignKey(entity => entity.GalleryId);
        }
    }
}
