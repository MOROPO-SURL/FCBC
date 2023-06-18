using System.Linq;

namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class CreatorDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Artist, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex Sex { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        public string Alias { get; set; }

        public string About { get; set; }

        public string Brand { get; set; }

        public Persistence.Enumerations.WorkPlace WorkPlace { get; set; }

        public ArtWorkManifestationDisplayApiModel[] ArtWorkManifestations { get; set; }

        public CraftWorkManifestationDisplayApiModel[] CraftWorkManifestations { get; set; }

        public ServiceManifestationDisplayApiModel[] ServiceManifestations { get; set; }

        public void Import(Persistence.Entities.Artist entity)
        {
            Id = entity.Id;
            Name = entity.Identity.Name;
            LastName = entity.Identity.LastName;
            Sex = entity.Identity.Sex;
            ImageId = entity.Identity.ImageId;
            Alias = entity.Identity.Alias;
            About = entity.Identity.About;
            Brand = entity.Brand;
        }

        public override string ToString()
        {
            return LastName == null ? Name : string.Format("{0} {1}", Name, LastName);
        }
    }
}
