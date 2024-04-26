using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class CreationGroupTranslationEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<CreationGroupTranslation>
    {
        protected override void ApplayRules(EntityTypeBuilder<CreationGroupTranslation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.TranslatableLanguage, entity.CreationGroupId });

            entityTypeBuilder.HasOne(entity => entity.CreationGroup).WithMany(entity => entity.Translations).HasForeignKey(entity => entity.CreationGroupId);



        }
    }
}
