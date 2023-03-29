namespace MOROPO.FCBC.Galleries.Models.Page
{
    public class MainPageModel : SmartSolucionesCuba.XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private string name;
        private System.DateTime? bornDate;
        private string about;
        private string history;
        private FFImageLoading.Work.ImageSource image;

        public string Name { get => name; set => SetProperty(ref name, value); }

        public System.DateTime? BornDate { get => bornDate; set => SetProperty(ref bornDate, value); }

        public string About { get => about; set => SetProperty(ref about, value); }

        public string History { get => history; set => SetProperty(ref history, value); }

        public FFImageLoading.Work.ImageSource Image { get => image; set => SetProperty(ref image, value); }
    }
}
