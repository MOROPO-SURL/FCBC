namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtisticManifestationTranslationsManagementViewModel
    {
        public ArtisticManifestationDisplayViewModel ArtisticManifestation { get; set; }

        public ArtisticManifestationTranslationDisplayViewModel[] Translations { get; set; }

        public Persistence.Enumerations.TranslatableLanguage[] NotSelectedLanguages { get; set; }

    }
}
