using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtWorkTranslationDisplayViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
       
        [Display(Name = "ArtWork")]
        public ArtWork ArtWork { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public ArtWorkLanguageDisplayViewModel ArtWorkLanguege { get; set; }
        public string Collection { get; set; }
        public string Location { get; set; }


        public void Import(ArtWorkTranslation entity)
        {
            ArtWork = entity.ArtWork;
            Name = entity.Name;
            TranslatableLanguage = entity.TranslatableLanguage;
            Collection = entity.Collection;
            Location = entity.Location;
        }

        public ArtWorkTranslationInputViewModel MergeViewModel()
        {
            ArtWorkTranslationInputViewModel viewModel = new ArtWorkTranslationInputViewModel();
            viewModel.Name = Name;
            viewModel.ArtWork = ArtWork;
            viewModel.TranslatableLanguage = TranslatableLanguage;
            viewModel.ArtWorkLanguege = ArtWorkLanguege;
            viewModel.Collection = Collection;
            viewModel.Loaction = Location;
            return viewModel;
        }
    }
}
