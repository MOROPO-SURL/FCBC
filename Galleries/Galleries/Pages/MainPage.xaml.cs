using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

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

            Refresh();
        }

        private void ExposureDetailButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushPageAsync(new ExposureDetailPage(BindingContextPageModel.ImageSource, ((sender as Xamarin.Forms.BindableObject).BindingContext as SGI.API.Common.Models.ExposureDisplayApiModel).Id));
        }

        private void ContactHeaderButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushPageAsync(new ContactPage(BindingContextPageModel));
        }

        private void HistoryHeaderButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushPageAsync(new HistoryPage(BindingContextPageModel.History));
        }

        private void RefreshToolbarItem_Clicked(object sender, System.EventArgs eventArgs)
        {
            if (BindingContextPageModel.IsBusy) return;

            Refresh();
        }

        private void LanguageToolbarItem_Clicked(object sender, System.EventArgs eventArgs)
        {
            if (BindingContextPageModel.IsBusy) return;

            DisplayActionSheet(AppResources.PickLanguageHeader, AppResources.CancelText, null, AppResources.LanguageNameSpanish, AppResources.LanguageNameEnglish, AppResources.LanguageNameFrench, AppResources.LanguageNameRussian).ContinueWith(task => 
            {
                if (string.IsNullOrEmpty(task.Result) || task.Result.Equals(AppResources.CancelText)) return;

                if (task.Result.Equals(AppResources.LanguageNameSpanish))
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es");
                }
                else if (task.Result.Equals(AppResources.LanguageNameEnglish))
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                }
                else if (task.Result.Equals(AppResources.LanguageNameFrench))
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr");
                }
                else if (task.Result.Equals(AppResources.LanguageNameRussian))
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru");
                }

                AppResources.Culture = System.Threading.Thread.CurrentThread.CurrentUICulture;

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    Xamarin.Forms.Application.Current.MainPage = new Xamarin.Forms.NavigationPage(new MainPage());
                });
            });
        }

        private void Refresh()
        {
            BindingContextPageModel.IsBusy = true;
            IsEnabled = false;

            var galleryApiConnectClient = new SGI.API.Connect.Clients.GalleryConsultApiConnectClient(Providers.HttpClientHelperProvider.Instance);

            galleryApiConnectClient.GetAsync(SGI.Persistence.Enumerations.GalleryId.Amalia).ContinueWith(getGalleryTask =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (getGalleryTask.IsFaulted || getGalleryTask.IsCanceled)
                    {
                        Helpers.ExceptionHandlingHelper.Instance.DefaultHandle(getGalleryTask.Exception, Refresh);
                    }
                    else
                    {
                        BindingContextPageModel.About = getGalleryTask.Result.About;
                        BindingContextPageModel.History = getGalleryTask.Result.History;
                        BindingContextPageModel.BornDate = getGalleryTask.Result.BornDate;
                        BindingContextPageModel.ImageSource = getGalleryTask.Result.ImageId == null ? null : Xamarin.Forms.ImageSource.FromUri(new System.Uri(string.Format("{0}media/images/{1}.{2}", Constants.Networking.SGI_BASE_URL, getGalleryTask.Result.ImageId, getGalleryTask.Result.ImageExtension)));
                        BindingContextPageModel.EmailContacts = getGalleryTask.Result.EmailContacts;
                        BindingContextPageModel.PhoneContacts = getGalleryTask.Result.PhoneContacts;
                        BindingContextPageModel.WebContacts = getGalleryTask.Result.WebContacts;

                        BindingContextPageModel.IsBusy = false;

                        var exposuresApiConnectClient = new SGI.API.Connect.Clients.ExposureConsultApiConnectClient(Providers.HttpClientHelperProvider.Instance);

                        exposuresApiConnectClient.FilterPaginateAsync(new SGI.API.Common.Models.Filter.ExposureFilterApiModel { GalleryId = SGI.Persistence.Enumerations.GalleryId.Amalia }).ContinueWith(getExposuresTask =>
                        {
                            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                            {
                                if (getExposuresTask.IsFaulted || getExposuresTask.IsCanceled)
                                {
                                    Helpers.ExceptionHandlingHelper.Instance.DefaultHandle(getGalleryTask.Exception, Refresh);
                                }
                                else
                                {
                                    BindingContextPageModel.Exposures = getExposuresTask.Result.List;
                                }
                            });
                        });
                    }

                    IsEnabled = true;
                });
            });
        }
    }
}