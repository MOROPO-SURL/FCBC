namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class CriticDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Critic, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex Sex { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        public string Alias { get; set; }

        public string About { get; set; }

        public void Import(Persistence.Entities.Critic entity)
        {
            Id = entity.Id;
            Name = entity.Identity.Name;
            LastName = entity.Identity.LastName;
            Sex = entity.Identity.Sex;
            ImageId = entity.Identity.ImageId;
            Alias = entity.Identity.Alias;
            About = entity.Identity.About;
        }

        public override string ToString()
        {
            if (LastName != null) return string.Format("{0} {1}", Name, LastName);

            return Name;
        }
    }
}