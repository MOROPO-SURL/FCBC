using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Extensions;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class GalleryDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Gallery, Persistence.Enumerations.GalleryId>
    {
        public Persistence.Enumerations.GalleryId Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "FoundatedAt")]
        public System.DateTime? BornDate { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        [Display(Name = "History")]
        public string History { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        public void Import(Persistence.Entities.Gallery entity)
        {
            Id = entity.Id;
            Name = entity.Identity.Name;
            BornDate = entity.Identity.BornDate;
            About = entity.Identity.About;
            ImageId = entity.Identity.ImageId;
            ImageExtension = entity.Identity.Image?.ContentType.ContentTypeToExtension();
            History = entity.History;
        }
    }
}
