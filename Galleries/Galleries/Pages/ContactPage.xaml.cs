using MOROPO.FCBC.SGI.API.Common.Models;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace MOROPO.FCBC.Galleries.Pages
{
    public partial class ContactPage : SmartSolucionesCuba.XFToolKit.Mvvm.Pages.BindingContextedContentPage<Models.Page.MainPageModel>
    {
        public ContactPage(Models.Page.MainPageModel model) : base(model) 
        {
            InitializeComponent();

            BrandImage.Source = Xamarin.Forms.ImageSource.FromResource("MOROPO.FCBC.Galleries.Assets.Image.Identity_Primary_FCBC.png");
        }

        private void PhoneContactItemActionButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            Xamarin.Essentials.PhoneDialer.Open(((sender as Xamarin.Forms.BindableObject).BindingContext as PhoneContactItemDisplayApiModel).PhoneNumber);
        }

        private void EmailContactItemActionButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            Xamarin.Essentials.Email.ComposeAsync(new Xamarin.Essentials.EmailMessage
            {
                To = new System.Collections.Generic.List<string> { ((sender as Xamarin.Forms.BindableObject).BindingContext as EmailContactItemDisplayApiModel).EmailAddress }
            });
        }

        private void WebContactItemActionButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            Xamarin.Essentials.Browser.OpenAsync(new System.Uri(((sender as Xamarin.Forms.BindableObject).BindingContext as WebContactItemDisplayApiModel).URL), Xamarin.Essentials.BrowserLaunchMode.External);
        }

        private void BackButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPopPageAsync();
        }
    }
}