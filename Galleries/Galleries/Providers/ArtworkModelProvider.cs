using FFImageLoading;

namespace MOROPO.FCBC.Galleries.Providers
{
    internal sealed class ArtworkModelProvider
    {
        private static ArtworkModelProvider _instance;

        public static ArtworkModelProvider Instance => _instance ?? (_instance = new ArtworkModelProvider());

        private readonly System.Collections.Generic.List<Models.Common.ImageCommonModel> imageRawModels;

        public ArtworkModelProvider()
        {
            imageRawModels = new System.Collections.Generic.List<Models.Common.ImageCommonModel>();
        }

        public async System.Threading.Tasks.Task LoadAsync()
        {
            imageRawModels.Clear();

            for (int i = 20; i <= 37; i++)
            {
                var extension = "jpg";

                //if (i == 4 || i == 14 || i == 18) extension = "png";

                var model = new Models.Common.ImageCommonModel
                {
                    Url = string.Format("https://fcbc-sgi.moropo.cu/media/images/_{0}.{1}", i, extension)
                };

                System.Diagnostics.Debug.WriteLine(model.Url);

                var cacheStream = await ImageService.Instance.Config.DownloadCache.DownloadAndCacheIfNeededAsync(
                    model.Url, 
                    ImageService.Instance.LoadUrl(model.Url), 
                    ImageService.Instance.Config, 
                    System.Threading.CancellationToken.None);

                using (cacheStream.ImageStream)
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    await cacheStream.ImageStream.CopyToAsync(memoryStream);
                    
                    model.Data = memoryStream.ToArray();
                }

                await ImageService.Instance.LoadUrl(model.Url).Success((info, result) =>
                {
                    model.Width = info.CurrentWidth;
                    model.Height = info.CurrentHeight;
                }).PreloadAsync();

                imageRawModels.Add(model);
            }
        }

        public Models.Common.ImageCommonModel[] GetImageRawModels()
        {
            return imageRawModels.ToArray();
        }
    }
}
