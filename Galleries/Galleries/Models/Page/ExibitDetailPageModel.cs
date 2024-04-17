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

        public object[] MediaItems
        {
            get
            {
                var result = new System.Collections.Generic.List<object>();

                if (Work != null)
                {
                    result.Add(Work.FeatureImage);

                    foreach (var item in Work.Videos)
                    {
                        result.Add(item);
                    }

                    foreach (var item in Work.Images)
                    {
                        if (item.Id == Work.FeatureImage.Id) continue;

                        result.Add(item);
                    }
                }

                return result.ToArray();
            }
        }

        public bool HasDimensions { get { return Work.Width != null || Work.Height != null || Work.Weight != null || Work.Profundity != null; } }
    }
}
