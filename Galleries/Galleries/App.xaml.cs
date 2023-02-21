namespace MOROPO.FCBC.Galleries
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Pages.MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
