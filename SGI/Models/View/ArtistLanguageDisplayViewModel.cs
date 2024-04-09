namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistLanguageDisplayViewModel
    {
        public System.Guid ArtistId { get; set; }
        public string Name { get; set; }
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
    }
}
