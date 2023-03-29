namespace MOROPO.FCBC.Galleries.Pages
{
    public partial class MainPage : SmartSolucionesCuba.XFToolKit.Mvvm.Pages.BindingContextedContentPage<Models.Page.MainPageModel>
    {
        public MainPage() : base(new Models.Page.MainPageModel { IsBusy = true })
        {
            InitializeComponent();

            IsEnabled = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            IsEnabled = true;

            var apiCOnnectClient = new SGI.API.Connect.Clients.GalleryConsultApiConnectClient(Providers.HttpClientHelpeProvider.Instance);

            apiCOnnectClient.GetAsync(SGI.Persistence.Enumerations.GalleryId.Amalia).ContinueWith(task =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (task.IsFaulted || task.IsCanceled)
                    {
                        Helpers.ExceptionHandlingHelper.Instance.DefaultHandle(task.Exception);
                    }
                    else
                    {

                    }

                    BindingContextPageModel.IsBusy = false;
                });
            });
        }
    }
}