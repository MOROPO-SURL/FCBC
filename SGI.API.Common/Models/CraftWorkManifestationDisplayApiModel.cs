namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class CraftWorkManifestationDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.CraftManifestation, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public void Import(Persistence.Entities.CraftManifestation entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
