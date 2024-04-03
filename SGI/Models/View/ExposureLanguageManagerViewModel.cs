namespace MOROPO.FCBC.SGI.Models.View
{
    public class ExposureLanguageManagerViewModel
    {
        public ExposureDisplayViewModel Exposure { get; set; }

        public System.Collections.Generic.ICollection<ExposureTranslationDisplayViewModel> Translations { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> NotSelectedLanguage { get; set; }
    }
}
