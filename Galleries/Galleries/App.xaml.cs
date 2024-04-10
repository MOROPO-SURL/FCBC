namespace MOROPO.FCBC.Galleries
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(System.Globalization.CultureInfo.InstalledUICulture.TwoLetterISOLanguageName);

            AppResources.Culture = System.Threading.Thread.CurrentThread.CurrentUICulture;

            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                FFImageLoading.ImageService.Instance.Initialize(new FFImageLoading.Config.Configuration
                {
                    HttpClient = new System.Net.Http.HttpClient(Xamarin.Forms.DependencyService.Get<Providers.Facade.INativeHttpClientHandlerProviderFacade>().Get())
                });
            }

            InitializeComponent();

            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                FFImageLoading.ImageService.Instance.Config.HttpClient = new System.Net.Http.HttpClient(Xamarin.Forms.DependencyService.Get<Providers.Facade.INativeHttpClientHandlerProviderFacade>().Get());
            }

            MainPage = new Xamarin.Forms.NavigationPage(new Pages.MainPage());
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
