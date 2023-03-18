namespace MOROPO.FCBC.SGI.Models.View
{
    public class CraftManifestationGroupDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.CraftManifestationGroup, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public CraftManifestationGroupDisplayViewModel Parent { get; set; }

        public string Description { get; set; }

        public void Import(Persistence.Entities.CraftManifestationGroup entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
