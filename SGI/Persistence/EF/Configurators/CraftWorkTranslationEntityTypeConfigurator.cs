using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class CraftWorkTranslationEntityTypeConfigurator: SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<CraftWorkTranslation>
    {
        protected override void ApplayRules(EntityTypeBuilder<CraftWorkTranslation> entityTypeBuilder)
    {
        base.ApplayRules(entityTypeBuilder);

        entityTypeBuilder.HasKey(entity => new { entity.TranslatableLanguage, entity.CraftWorkId });

        entityTypeBuilder.HasOne(entity => entity.CraftWork).WithMany(entity => entity.Translations).HasForeignKey(entity => entity.CraftWorkId);



    }
}
}
