using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class CraftManifestationTranslationEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<CraftManifestationTranslation>
    {
        protected override void ApplayRules(EntityTypeBuilder<CraftManifestationTranslation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.TranslatableLanguage, entity.CraftManifestationId });

            entityTypeBuilder.HasOne(entity => entity.CraftManifestation).WithMany(entity => entity.Translations).HasForeignKey(entity => entity.CraftManifestationId);
        }
    }
}
