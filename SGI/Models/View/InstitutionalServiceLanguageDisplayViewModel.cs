namespace MOROPO.FCBC.SGI.Models.View
{
    public class InstitutionalServiceLanguageDisplayViewModel
    {
        public System.Guid InstitutionalServiceId { get; set; }
        public string Name { get; set; }
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
    }
}
