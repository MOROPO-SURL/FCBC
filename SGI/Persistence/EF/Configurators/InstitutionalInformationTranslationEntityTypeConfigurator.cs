using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Persistence.EF.Configurators
{
    public class InstitutionalInformationTranslationEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<InstitutionalInformationTranslation>
    {
        protected override void ApplayRules(EntityTypeBuilder<InstitutionalInformationTranslation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.TranslatableLanguage, entity.InstitutionalInformationKind });

            entityTypeBuilder.HasOne(entity => entity.InstitutionalInformation).WithMany(entity => entity.Translations).HasForeignKey(entity => entity.InstitutionalInformationKind);



        }
    }
}
