namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtisticManifestationGroupDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.ArtisticManifestationGroup, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string ParentName { get; set; }

        public void Import(Persistence.Entities.ArtisticManifestationGroup entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            ParentName = entity.Parent?.Name;
        }
    }
}
