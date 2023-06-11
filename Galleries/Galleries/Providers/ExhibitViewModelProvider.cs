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
                            Id = _model.FeatureImage.Id
                        },
                        Creators = _model.Creators,
                        ArtTechniques = _model.ArtTechniques.Select(item => item.ToString()).ToArray(),
                        Year = _model.Year,
                        About = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
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
