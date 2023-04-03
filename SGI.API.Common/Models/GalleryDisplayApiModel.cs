namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class GalleryDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Gallery, Persistence.Enumerations.GalleryId>
    {
        public Persistence.Enumerations.GalleryId Id { get; set; }

        public string Name { get; set; }

        public System.DateTime? BornDate { get; set; }

        public string About { get; set; }

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
            History = entity.History;
        }
    }
}
