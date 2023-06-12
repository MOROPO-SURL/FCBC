using FFImageLoading;
using System.Linq;

namespace MOROPO.FCBC.Galleries.Providers
{
    internal sealed class ExhibitViewModelProvider
    {
        private static ExhibitViewModelProvider _instance;

        public static ExhibitViewModelProvider Instance => _instance ?? (_instance = new ExhibitViewModelProvider());

        private readonly System.Collections.Generic.List<Models.View.ExhibitViewModel> exhibits;

        private ExhibitViewModelProvider()
        {
            exhibits = new System.Collections.Generic.List<Models.View.ExhibitViewModel>();
        }

        public async System.Threading.Tasks.Task PrepareAsync(SGI.API.Common.Models.ExhibitDisplayApiModel[] models)
        {
            exhibits.Clear();

            foreach (var model in models)
            {
                var exhibit = new Models.View.ExhibitViewModel
                {
                    Id = model.Id,
                    Index = model.Index
                };

                foreach (var _model in model.ArtWorks)
                {
                    var artWork = new Models.View.ArtWorkViewModel
                    {
                        Id = _model.Id,
                        Title = _model.Title,
                        FeatureImage = new Models.View.ImageViewModel
                        {
                            Id = _model.FeatureImage.Id,
                            Extension = _model.FeatureImage.Extension
                        },
                        Creators = _model.Creators,
                        Techniques = _model.ArtTechniques.ToArray(),
                        Year = _model.Year,
                        Collection = _model.Collection,
                        Location = _model.Location,
                        Manifestation = _model.Manifestation,
                        About = _model.About,
                        Width = _model.Width,
                        Height = _model.Height,
                        Profundity = _model.Profundity,
                        DimensionsUnitMeasurement = _model.DimensionsUnitMeasurement,
                        Weight = _model.Weight,
                        WeightUnitMeasurement = _model.WeightUnitMeasurement,
                        Images = _model.Images,
                        Materials = _model.Materials,
                        Themathics = _model.Themathics,
                        ArtWorkAwards = _model.ArtWorkAwards,
                        Valorations = _model.Valorations
                    };

                    var url = string.Format("{0}media/images/{1}.{2}", Constants.Networking.SGI_BASE_URL, _model.FeatureImage.Id, _model.FeatureImage.Extension);

                    var cacheStream = await ImageService.Instance.Config.DownloadCache.DownloadAndCacheIfNeededAsync(
                        url,
                        ImageService.Instance.LoadUrl(url),
                        ImageService.Instance.Config,
                        System.Threading.CancellationToken.None);

                    using (cacheStream.ImageStream)
                    using (var memoryStream = new System.IO.MemoryStream())
                    {
                        await cacheStream.ImageStream.CopyToAsync(memoryStream);

                        artWork.FeatureImage.Data = memoryStream.ToArray();
                    }

                    await ImageService.Instance.LoadUrl(url).Success((info, result) =>
                    {
                        artWork.FeatureImage.Width = info.CurrentWidth;
                        artWork.FeatureImage.Height = info.CurrentHeight;
                    }).PreloadAsync();

                    exhibit.ArtWorks.Add(artWork);
                }

                exhibits.Add(exhibit);
            }
        }

        public Models.View.ExhibitViewModel[] GetExhibits()
        {
            return exhibits.ToArray();
        }
    }
}
