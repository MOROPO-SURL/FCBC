namespace MOROPO.FCBC.Galleries.Pages
{
    public partial class ExibitDetailPage : SmartSolucionesCuba.XFToolKit.Mvvm.Pages.BindingContextedContentPage<Models.Page.ExibitDetailPageModel>
    {
        public ExibitDetailPage(Models.View.ArtWorkViewModel model) : base(new Models.Page.ExibitDetailPageModel { Work = model })
        {
            InitializeComponent();

            if (model.Images.Length <=0 && model.Videos.Length <= 0) 
            {
                MediaItemsCarouselView.Loop = false;
                MediaItemsCarouselView.IsSwipeEnabled = false;
                MediaItemsIndicatorView.IsVisible = false;
                PreviuesMediaItemButton.IsVisible = false;
                NextMediaItemButton.IsVisible = false;
            }
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

        private void PreviuesMediaItemButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            var newPosition = MediaItemsCarouselView.Position - 1;

            if (newPosition < 0)
            {
                newPosition = BindingContextPageModel.MediaItems.Length - 1;
            }

            MediaItemsCarouselView.CurrentItem = BindingContextPageModel.MediaItems[newPosition];
        }

        private void NextMediaItemButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            var newPosition = MediaItemsCarouselView.Position + 1;

            if (newPosition >= BindingContextPageModel.MediaItems.Length)
            {
                newPosition = 0;
            }

            MediaItemsCarouselView.CurrentItem = BindingContextPageModel.MediaItems[newPosition];
        }
    }
}