namespace MOROPO.FCBC.Galleries.Pages
{
    public partial class ExibitDetailPage : SmartSolucionesCuba.XFToolKit.Mvvm.Pages.BindingContextedContentPage<Models.Page.ExibitDetailPageModel>
    {
        public ExibitDetailPage(Models.View.ArtWorkViewModel model) : base(new Models.Page.ExibitDetailPageModel { Work = model })
        {
            InitializeComponent();
        }

        private void CreatorView_Tapped(object sender, System.EventArgs eventArgs)
        {
            BindingContextPageModel.PresentedCreator = (sender as Xamarin.Forms.BindableObject).BindingContext as SGI.API.Common.Models.CreatorDisplayApiModel;
        }

        private void DismissPresentedCreatorButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            BindingContextPageModel.PresentedCreator = null;
        }

        private void CriticView_Tapped(object sender, System.EventArgs eventArgs)
        {
            BindingContextPageModel.PresentedCritic = ((sender as Xamarin.Forms.BindableObject).BindingContext as SGI.API.Common.Models.ValorationDisplayApiModel).Critic;
        }

        private void DismissPresentedCriticButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            BindingContextPageModel.PresentedCritic = null;
        }
    }
}