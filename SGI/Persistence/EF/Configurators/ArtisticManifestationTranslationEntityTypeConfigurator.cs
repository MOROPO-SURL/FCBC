using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ArtisticManifestationTranslationEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<ArtisticManifestationTranslation>
    {
        protected override void ApplayRules(EntityTypeBuilder<ArtisticManifestationTranslation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.TranslatableLanguage, entity.ArtisticManifestationId });

            entityTypeBuilder.HasOne(entity => entity.ArtisticManifestation).WithMany(entity => entity.Translations).HasForeignKey(entity => entity.ArtisticManifestationId);
        }

    }
}
