namespace MOROPO.FCBC.SGI.Managers
{
    public class RelativeAttachmentFileManager
    {
        private const string MEDIA_ROOT_DIRECTORY_NAME = "media";
        private const string DOCUMENTS_DIRECTORY_NAME = "attachments";

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment env;

        public RelativeAttachmentFileManager(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.env = env;
        }

        public void DeleteFile(Persistence.Entities.Contract.AbstractAttachment attachment)
        {
            System.IO.File.Delete(GetAbsoluteFilePath(attachment));
        }

        public string GetAbsoluteFilePath(Persistence.Entities.Contract.AbstractAttachment attachment)
        {
            return GetAbsoluteFilePath(attachment.Id, attachment.ContentType);
        }

        public byte[] GetFileContent(Persistence.Entities.Contract.AbstractAttachment attachment)
        {
            byte[] result;

            using (var file = System.IO.File.OpenRead(GetAbsoluteFilePath(attachment)))
            {
                result = new byte[file.Length];

                file.Read(result, 0, (int)file.Length);

                file.Close();
            }

            return result;
        }

        private string GetAbsoluteFilePath(System.Guid id, string contentType)
        {
            return env.WebRootFileProvider.GetFileInfo(System.IO.Path.Combine(MEDIA_ROOT_DIRECTORY_NAME, DOCUMENTS_DIRECTORY_NAME, string.Format("{0}.{1}", id, contentType.Split('/')[1].ToLower()))).PhysicalPath;
        }

        public string GetRelativeFilePath(Persistence.Entities.Contract.AbstractAttachment attachment)
        {
            return null;
        }

        public void SaveFileContent(System.Guid attachmentId, byte[] content, string contentType)
        {
            System.IO.File.WriteAllBytes(GetAbsoluteFilePath(attachmentId, contentType ), content);
        }
    }
}
