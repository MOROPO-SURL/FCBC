namespace MOROPO.FCBC.SGI.Models.View
{
    public class CraftManifestationTranslationsManagementViewModel
    {
        public CraftManifestationDisplayViewModel CraftManifestation { get; set; }

        public CraftManifestationTranslationDisplayViewModel[] Translations { get; set; }

        public Persistence.Enumerations.TranslatableLanguage[] NotSelectedLanguages { get; set; }
    }
}
