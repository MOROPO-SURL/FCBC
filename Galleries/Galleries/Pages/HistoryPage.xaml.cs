using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace MOROPO.FCBC.Galleries.Pages
{
    public partial class HistoryPage : SmartSolucionesCuba.XFToolKit.Mvvm.Pages.ContentPage
    {
        public HistoryPage(string history)
        {
            InitializeComponent();

            HistoryLabel.Text = history;
        }

        private void BackButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPopPageAsync();
        }
    }
}