using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ThematicTranslationsEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<ThematicTranslation>
    {
        protected override void ApplayRules(EntityTypeBuilder<ThematicTranslation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.TranslatableLanguage, entity.ThematicId });

            entityTypeBuilder.HasOne(entity => entity.Thematic).WithMany(entity => entity.Translations).HasForeignKey(entity => entity.ThematicId);



        }
    }

}
