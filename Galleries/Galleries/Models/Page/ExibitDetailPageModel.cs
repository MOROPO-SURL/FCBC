using MOROPO.FCBC.SGI.API.Common.Models;

namespace MOROPO.FCBC.Galleries.Models.Page
{
    public class ExibitDetailPageModel : SmartSolucionesCuba.XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private CriticDisplayApiModel presentedCritic;
        private CreatorDisplayApiModel presentedCreator;

        public CriticDisplayApiModel PresentedCritic { get => presentedCritic; set => SetProperty(ref presentedCritic, value); }

        public CreatorDisplayApiModel PresentedCreator { get => presentedCreator; set =>SetProperty(ref presentedCreator, value); }

        public View.ArtWorkViewModel Work { get; set; }

        public bool HasDimensions { get { return Work.Width != null || Work.Height != null || Work.Weight != null || Work.Profundity != null; } }
    }
}
