namespace MOROPO.FCBC.Galleries.Models.Page
{
    public class MainPageModel : SmartSolucionesCuba.XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private string name;
        private System.DateTime? bornDate;
        private string about;
        private string history;
        private Xamarin.Forms.ImageSource image;
        private System.Collections.Generic.List<SGI.API.Common.Models.ExposureDisplayApiModel> exposures;

        public string Name { get => name; set => SetProperty(ref name, value); }

        public System.DateTime? BornDate { get => bornDate; set => SetProperty(ref bornDate, value); }

        public string About { get => about; set => SetProperty(ref about, value); }

        public string History { get => history; set => SetProperty(ref history, value); }

        public Xamarin.Forms.ImageSource ImageSource { get => image; set => SetProperty(ref image, value); }

        public System.Collections.Generic.List<SGI.API.Common.Models.ExposureDisplayApiModel> Exposures { get => exposures; set => SetProperty(ref exposures, value); }
    }
}
