namespace MOROPO.FCBC.SGI.Models.View
{
    public class ServiceTechniqueDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.ServiceTechnique, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public void Import(Persistence.Entities.ServiceTechnique entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
