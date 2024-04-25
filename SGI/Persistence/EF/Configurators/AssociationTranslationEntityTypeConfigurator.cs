using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class AssociationTranslationEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<AssociationTranslation>
    {
        protected override void ApplayRules(EntityTypeBuilder<AssociationTranslation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.TranslatableLanguage, entity.AssociationId });

            entityTypeBuilder.HasOne(entity => entity.Association).WithMany(entity => entity.Translations).HasForeignKey(entity => entity.AssociationId);



        }
    }
}
