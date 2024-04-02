using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class GalleryTranslationDisplayViewModel
    {
        public Persistence.Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }

        [Display(Name = "Gallery")]
        public Gallery Gallery { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public GalleryLanguageDisplayViewModel GalleryLanguege { get; set; }

        public void Import(GalleryTranslation entity)
        {
            Gallery = entity.Gallery;
            Name = entity.Name;
            TranslatableLanguage = entity.TranslatableLanguage;
        }

        public GalleryTranslationInputViewModel MergeViewModel()
        {
            GalleryTranslationInputViewModel viewModel = new GalleryTranslationInputViewModel();
            viewModel.Name = Name;
            viewModel.Gallery = Gallery;
            viewModel.TranslatableLanguage = TranslatableLanguage;
            viewModel.GalleryLanguege = GalleryLanguege;
            return viewModel;
        }
    }
}
