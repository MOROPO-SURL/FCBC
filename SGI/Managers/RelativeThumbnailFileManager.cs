using MOROPO.FCBC.SGI.Extensions;

namespace MOROPO.FCBC.SGI.Managers
{
    public class RelativeThumbnailFileManager : SAPRESSC.Middleware.Media.Web.Common.Managers.Facade.IThumbnailFileManagerFacade
    {
        private const string MEDIA_ROOT_DIRECTORY_NAME = "media";
        private const string THUMBNAILS_DIRECTORY_NAME = "thumbnails";

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment env;

        public RelativeThumbnailFileManager(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.env = env;
        }

        public void DeleteThumbnailFile(MOROPO.SAPRESSC.Middleware.Media.Persistence.Entities.Thumbnail thumbnail)
        {
            System.IO.File.Delete(GetThumbnailAbsoluteFilePath(thumbnail));
        }

        public string GetThumbnailAbsoluteFilePath(MOROPO.SAPRESSC.Middleware.Media.Persistence.Entities.Thumbnail thumbnail)
        {
            return env.WebRootFileProvider.GetFileInfo(System.IO.Path.Combine(MEDIA_ROOT_DIRECTORY_NAME, THUMBNAILS_DIRECTORY_NAME, string.Format("{0}.{1}", thumbnail.Id, thumbnail.ContentType.ContentTypeToExtension()))).PhysicalPath;
        }

        public byte[] GetThumbnailFileContent(MOROPO.SAPRESSC.Middleware.Media.Persistence.Entities.Thumbnail thumbnail)
        {
            byte[] result;

            using (var file = System.IO.File.OpenRead(GetThumbnailAbsoluteFilePath(thumbnail)))
            {
                result = new byte[file.Length];

                file.Read(result, 0, (int)file.Length);

                file.Close();
            }

            return result;
        }

        public string GetThumbnailRelativeFilePath(MOROPO.SAPRESSC.Middleware.Media.Persistence.Entities.Thumbnail thumbnail)
        {
            return null;
        }

        public void SaveThumbnailFileContent(System.Guid thumbnailId, byte[] content, string contentType)
        {
            System.IO.File.WriteAllBytes(GetThumbnailAbsoluteFilePath(new SAPRESSC.Middleware.Media.Persistence.Entities.Thumbnail { Id = thumbnailId, ContentType = contentType }), content);
        }
    }
}
