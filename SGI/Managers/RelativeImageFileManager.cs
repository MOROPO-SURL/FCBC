using MOROPO.FCBC.SGI.Extensions;

namespace MOROPO.FCBC.SGI.Managers
{
    public class RelativeImageFileManager : SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade.IImageFileManagerFacade
    {
        private const string MEDIA_ROOT_DIRECTORY_NAME = "media";
        private const string IMAGES_DIRECTORY_NAME = "images";

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment env;

        public RelativeImageFileManager(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.env = env;
        }

        public void DeleteImageFile(SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities.Image image)
        {
            System.IO.File.Delete(GetImageAbsoluteFilePath(image));
        }

        public string GetImageAbsoluteFilePath(SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities.Image image)
        {
            return env.WebRootFileProvider.GetFileInfo(System.IO.Path.Combine(MEDIA_ROOT_DIRECTORY_NAME, IMAGES_DIRECTORY_NAME, string.Format("{0}.{1}", image.Id, image.ContentType.ContentTypeToExtension()))).PhysicalPath;
        }

        public byte[] GetImageFileContent(SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities.Image image)
        {
            byte[] result;

            using (var file = System.IO.File.OpenRead(GetImageAbsoluteFilePath(image)))
            {
                result = new byte[file.Length];

                file.Read(result, 0, (int)file.Length);

                file.Close();
            }

            return result;
        }

        public string GetImageRelativeFilePath(SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities.Image image)
        {
            return null;
        }

        public void SaveImageFileContent(System.Guid imageId, byte[] content, string contentType)
        {
            System.IO.File.WriteAllBytes(GetImageAbsoluteFilePath(new SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities.Image { Id = imageId, ContentType = contentType }), content);
        }
    }
}
