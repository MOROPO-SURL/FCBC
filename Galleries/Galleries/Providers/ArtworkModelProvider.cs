using FFImageLoading;
using System.Linq;

namespace MOROPO.FCBC.Galleries.Providers
{
    internal sealed class ArtworkModelProvider
    {
        private static ArtworkModelProvider _instance;

        public static ArtworkModelProvider Instance => _instance ?? (_instance = new ArtworkModelProvider());

        private readonly System.Collections.Generic.List<Models.Raw.ImageRawModel> imageRawModels;

        public ArtworkModelProvider()
        {
            imageRawModels = new System.Collections.Generic.List<Models.Raw.ImageRawModel>();
        }

        public async System.Threading.Tasks.Task LoadAsync()
        {
            imageRawModels.Clear();

            for (int i = 1; i <= 18; i++)
            {
                var extension = "jpg";

                if (i == 4 || i == 14 || i == 18) extension = "png";

                var model = new Models.Raw.ImageRawModel
                {
                    Url = string.Format("http://localhost/media/images/_{0}.{1}", i, extension)
                };

                var cacheStream = await ImageService.Instance.Config.DownloadCache.DownloadAndCacheIfNeededAsync(
                    model.Url, 
                    ImageService.Instance.LoadUrl(model.Url).Success(LoadSuccess_Callback), 
                    ImageService.Instance.Config, 
                    System.Threading.CancellationToken.None);

                using (cacheStream.ImageStream)
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    await cacheStream.ImageStream.CopyToAsync(memoryStream);
                    
                    model.Data = memoryStream.ToArray();
                }

                imageRawModels.Add(model);
            }
        }

        public Models.Raw.ImageRawModel[] GetImageRawModels()
        {
            return imageRawModels.ToArray();
        }

        private void LoadSuccess_Callback(FFImageLoading.Work.ImageInformation info, FFImageLoading.Work.LoadingResult result)
        {
            var model = imageRawModels.FirstOrDefault(item => item.Url.Equals(info.CacheKey));

            model.Width = info.CurrentWidth;
            model.Height = info.CurrentHeight;
        }
    }
}
