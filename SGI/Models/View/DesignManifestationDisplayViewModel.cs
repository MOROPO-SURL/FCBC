namespace MOROPO.FCBC.SGI.Models.View
{
    public class DesignManifestationDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.DesignManifestation, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public DesignManifestationGroupDisplayViewModel Group { get; set; }

        public string Description { get; set; }

        public void Import(Persistence.Entities.DesignManifestation entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
