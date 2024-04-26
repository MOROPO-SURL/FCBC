namespace MOROPO.FCBC.SGI.Models.View
{
    public class CraftTechniqueTranslationManagementViewModel
    {
        public CraftTechniqueDisplayViewModel CraftTechnique { get; set; }

        public CraftTechniqueTranslationDisplayViewModel[] Translations { get; set; }

        public Persistence.Enumerations.TranslatableLanguage[] NotSelectedLanguages { get; set; }
    }
}
