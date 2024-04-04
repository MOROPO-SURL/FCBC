using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class ActivityTranslationEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<ActivityTranslation>
    {
        protected override void ApplayRules(EntityTypeBuilder<ActivityTranslation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.TranslatableLanguage, entity.ActivityId });

            entityTypeBuilder.HasOne(entity => entity.Activity).WithMany(entity => entity.Translations).HasForeignKey(entity => entity.ActivityId);



        }
    }
}
