using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class MaterialTranslationEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<MaterialTranslation>
    {
        protected override void ApplayRules(EntityTypeBuilder<MaterialTranslation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.TranslatableLanguage, entity.MaterialId });

            entityTypeBuilder.HasOne(entity => entity.Material).WithMany(entity => entity.Translations).HasForeignKey(entity => entity.MaterialId);



        }
    }
}
