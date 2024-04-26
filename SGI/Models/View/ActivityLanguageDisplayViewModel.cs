namespace MOROPO.FCBC.SGI.Models.View
{
    public class ActivityLanguageDisplayViewModel
    {
        public System.Guid ActivityId { get; set; }
        public string Name { get; set; }
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
    }
}
