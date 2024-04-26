namespace MOROPO.FCBC.SGI.Models.View
{
    public class ThematicLanguageDisplayViewModel
    {
        public System.Guid ThematicId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
    }
}
