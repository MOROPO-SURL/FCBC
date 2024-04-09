using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistTranslationDisplayViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "Artist")]
        public Artist Artist { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public ArtistLanguageDisplayViewModel ArtistLanguege { get; set; }

        public void Import(ArtistTranslation entity)
        {
            Artist = entity.Artist;
            Name = entity.Name;
            TranslatableLanguage = entity.TranslatableLanguage;
        }

        public ArtistTranslationInputViewModel MergeViewModel()
        {
            ArtistTranslationInputViewModel viewModel = new ArtistTranslationInputViewModel();
            viewModel.Name = Name;
            viewModel.Artist = Artist;
            viewModel.TranslatableLanguage = TranslatableLanguage;
            viewModel.ArtistLanguege = ArtistLanguege;
            return viewModel;
        }
    }
}
