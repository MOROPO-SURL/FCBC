using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class CraftTechniqueTranslationEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<CraftTechniqueTranslation>
    {
        protected override void ApplayRules(EntityTypeBuilder<CraftTechniqueTranslation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.TranslatableLanguage, entity.CraftTechniqueId });

            entityTypeBuilder.HasOne(entity => entity.CraftTechnique).WithMany(entity => entity.Translations).HasForeignKey(entity => entity.CraftTechniqueId);
        }

    }
}
