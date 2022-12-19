namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtisticManifestationGroupDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.ArtisticManifestationGroup, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public ArtisticManifestationGroupDisplayViewModel Parent { get; set; }

        public string Description { get; set; }

        public void Import(Persistence.Entities.ArtisticManifestationGroup entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}