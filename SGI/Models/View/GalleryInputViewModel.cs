using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Extensions;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class GalleryInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Gallery, Persistence.Enumerations.GalleryId>
    {
        public Persistence.Enumerations.GalleryId Id { get; set; }

        [Display(Name = "FoundatedAt")]
        public System.DateTime? BornDate { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        [Display(Name = "History")]
        public string History { get; set; }

        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [FormFileMaxLength(Length = 3145728, ErrorMessage = "FileLimitSizeExcededMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        public bool ResetImage { get; set; }

        public Gallery Export()
        {
            var entity = new Gallery
            {
                Identity = new SAPRESSC.Middleware.Identity.Persistence.Entities.OrganizationIdentity()
            };

            Mergue(entity);

            return entity;
        }

        public void Import(Gallery entity)
        {
            Id = entity.Id;
            ImageId = entity.Identity.ImageId;
            ImageExtension = entity.Identity.Image?.ContentType.ContentTypeToExtension();
            BornDate = entity.Identity.BornDate;
            About = entity.Identity.About;
            History = entity.History;
        }

        public void Mergue(Gallery entity)
        {
            entity.Identity.BornDate = BornDate;
            entity.Identity.About = About;
            entity.History = History;
        }
    }
}
