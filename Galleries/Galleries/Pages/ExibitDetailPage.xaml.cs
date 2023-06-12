namespace MOROPO.FCBC.Galleries.Pages
{
    public partial class ExibitDetailPage : SmartSolucionesCuba.XFToolKit.Mvvm.Pages.ContentPage
    {
        public ExibitDetailPage(Models.View.ArtWorkViewModel model)
        {
            InitializeComponent();

            BindingContext = model;
        }
    }
}