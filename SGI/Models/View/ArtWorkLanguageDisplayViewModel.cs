namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtWorkLanguageDisplayViewModel
    {
        public System.Guid ArtWorkId { get; set; }
        public string Name { get; set; }
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
        public string Collection { get; set; }
        public string Location { get; set; }
    }
}
