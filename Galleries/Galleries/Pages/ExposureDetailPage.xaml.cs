using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;
using System.Linq;

namespace MOROPO.FCBC.Galleries.Pages
{
    public partial class ExposureDetailPage : SmartSolucionesCuba.XFToolKit.Mvvm.Pages.BindingContextedContentPage<Models.Page.ExposureDetailPageModel>
    {
        private const string CANCEL_OPTION = "Cancel";

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

            Refresh();            
        }

        private void GoToExposureButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            Urho.Color? frameBaseColor = null;

            if (!string.IsNullOrEmpty(BindingContextPageModel.Exposure.FrameBaseColor))
            {
                frameBaseColor = Urho.Color.FromHex(BindingContextPageModel.Exposure.FrameBaseColor);
            }

            this.NavigationPushModalPageAsync(new ExposureRoomPage(exposureId, frameBaseColor));
        }

        private void EmployeeView_Tapped(object sender, System.EventArgs eventArgs)
        {
            BindingContextPageModel.PresentedEmployee = (sender as Xamarin.Forms.BindableObject).BindingContext as SGI.API.Common.Models.EmployeeDisplayApiModel;
        }

        private void DismissPresentedEmployeeButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            BindingContextPageModel.PresentedEmployee = null;
        }

        private void EmployeeEmailContactButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            if (BindingContextPageModel.PresentedEmployee == null) return;

            if (BindingContextPageModel.PresentedEmployee.EmailContacts.Length == 0)
            {
                DisplayAlert("Aviso", "No se han especificado información para contactar por correo electrónico con este trabajador", "Aceptar");
            }

            if (BindingContextPageModel.PresentedEmployee.EmailContacts.Length == 1)
            {
                Xamarin.Essentials.Email.ComposeAsync(new Xamarin.Essentials.EmailMessage
                {
                    To = new System.Collections.Generic.List<string> { BindingContextPageModel.PresentedEmployee.EmailContacts[0].EmailAddress }
                });
            }
            else
            {
                DisplayActionSheet("Seleccionar dirección de correo electrónico:", CANCEL_OPTION, null, BindingContextPageModel.PresentedEmployee.EmailContacts.Select(item => item.EmailAddress).ToArray()).ContinueWith(task =>
                {
                    if (task.Result == CANCEL_OPTION) return;

                    Xamarin.Essentials.Email.ComposeAsync(new Xamarin.Essentials.EmailMessage
                    {
                        To = new System.Collections.Generic.List<string> { task.Result }
                    });
                });
            }
        }

        private void EmployeePhoneContactButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            if (BindingContextPageModel.PresentedEmployee == null) return;

            if (BindingContextPageModel.PresentedEmployee.PhoneContacts.Length == 0)
            {
                DisplayAlert("Aviso", "No se han especificado información para contactar por teléfono con este trabajador", "Aceptar");
            }

            if (BindingContextPageModel.PresentedEmployee.PhoneContacts.Length == 1)
            {
                try
                {
                    Xamarin.Essentials.PhoneDialer.Open(BindingContextPageModel.PresentedEmployee.PhoneContacts[0].PhoneNumber);
                }
                catch
                {
                    DisplayAlert("Aviso", "Desde este dispositivo no es posible realizar la llamada", "Aceptar");
                }
            }
            else
            {
                DisplayActionSheet("Seleccionar número de teléfono:", CANCEL_OPTION, null, BindingContextPageModel.PresentedEmployee.PhoneContacts.Select(item => item.PhoneNumber).ToArray()).ContinueWith(task =>
                {
                    if (task.Result == CANCEL_OPTION) return;

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() => Xamarin.Essentials.PhoneDialer.Open(task.Result));
                });
            }
        }

        private void Refresh()
        {
            BindingContextPageModel.IsBusy = true;

            var exposuresApiConnectClient = new SGI.API.Connect.Clients.ExposureConsultApiConnectClient(Providers.HttpClientHelperProvider.Instance);

            exposuresApiConnectClient.GetAsync(exposureId).ContinueWith(getExposuresTask =>
            {
                System.Threading.Thread.Sleep(System.TimeSpan.FromSeconds(2));

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (getExposuresTask.IsFaulted || getExposuresTask.IsCanceled)
                    {
                        Helpers.ExceptionHandlingHelper.Instance.DefaultHandle(getExposuresTask.Exception, Refresh);
                    }
                    else
                    {
                        BindingContextPageModel.Exposure = getExposuresTask.Result;

                        BindingContextPageModel.IsBusy = false;
                    }

                    IsEnabled = true;
                });
            });
        }
    }
}