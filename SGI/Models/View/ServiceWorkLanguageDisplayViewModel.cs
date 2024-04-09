namespace MOROPO.FCBC.SGI.Models.View
{
    public class ServiceWorkLanguageDisplayViewModel
    {
        public System.Guid ServiceWorkId { get; set; }
        public string Name { get; set; }
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
    }
}
