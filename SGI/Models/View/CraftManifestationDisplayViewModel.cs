namespace MOROPO.FCBC.SGI.Models.View
{
    public class CraftManifestationDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.CraftManifestation, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public CraftManifestationGroupDisplayViewModel Group { get; set; }

        public string Description { get; set; }

        public void Import(Persistence.Entities.CraftManifestation entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
