namespace MOROPO.FCBC.Galleries.Models.Page
{
    public class ExposureDetailPageModel : SmartSolucionesCuba.XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private SGI.API.Common.Models.ExposureDisplayApiModel exposure;

        public SGI.API.Common.Models.ExposureDisplayApiModel Exposure { get => exposure; set => SetProperty(ref exposure, value); }

        public Xamarin.Forms.ImageSource GalleyFeatureImageSource { get; set; }
    }
}
