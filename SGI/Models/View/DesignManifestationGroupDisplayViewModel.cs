namespace MOROPO.FCBC.SGI.Models.View
{
    public class DesignManifestationGroupDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.DesignManifestationGroup, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public DesignManifestationGroupDisplayViewModel Parent { get; set; }

        public string Description { get; set; }

        public void Import(Persistence.Entities.DesignManifestationGroup entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
