using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class CriticTranslationEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<CriticTranslation>
    {
        protected override void ApplayRules(EntityTypeBuilder<CriticTranslation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.TranslatableLanguage, entity.CriticId });

            entityTypeBuilder.HasOne(entity => entity.Critic).WithMany(entity => entity.Translations).HasForeignKey(entity => entity.CriticId);



        }
    }
}
