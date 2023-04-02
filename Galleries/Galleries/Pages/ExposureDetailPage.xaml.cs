using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace MOROPO.FCBC.Galleries.Pages
{
    public partial class ExposureDetailPage : SmartSolucionesCuba.XFToolKit.Mvvm.Pages.BindingContextedContentPage<Models.Page.ExposureDetailPageModel>
    {
        private System.Guid exposureId;

        public ExposureDetailPage(Xamarin.Forms.ImageSource galleryFeatureImageSource, System.Guid exposureId) : base(new Models.Page.ExposureDetailPageModel { IsBusy = true, GalleyFeatureImageSource = galleryFeatureImageSource })
        {
            this.exposureId = exposureId;

            InitializeComponent();

            IsEnabled = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            IsEnabled = true;

            var exposuresApiConnectClient = new SGI.API.Connect.Clients.ExposureConsultApiConnectClient(Providers.HttpClientHelpeProvider.Instance);

            exposuresApiConnectClient.GetAsync(exposureId).ContinueWith(getExposuresTask =>
            {
                System.Threading.Thread.Sleep(System.TimeSpan.FromSeconds(2));

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (getExposuresTask.IsFaulted || getExposuresTask.IsCanceled)
                    {
                        Helpers.ExceptionHandlingHelper.Instance.DefaultHandle(getExposuresTask.Exception);
                    }
                    else
                    {
                        BindingContextPageModel.Exposure = getExposuresTask.Result;

                        BindingContextPageModel.IsBusy = false;
                    }
                });
            });
        }

        private void GoToExposureButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushModalPageAsync(new ExposureRoomPage(exposureId));
        }
    }
}