namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtWorkLanguageDisplayViewModel
    {
        public System.Guid ArtWorkId { get; set; }
        public string Name { get; set; }
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
    }
}
