namespace MOROPO.FCBC.SGI.Models.View
{
    public class ServiceLineGroupDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.ServiceLineGroup, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public ServiceLineGroupDisplayViewModel Parent { get; set; }

        public string Description { get; set; }

        public void Import(Persistence.Entities.ServiceLineGroup entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
