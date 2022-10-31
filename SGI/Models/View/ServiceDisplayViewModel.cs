
namespace MOROPO.FCBC.SGI.Models.View
{
    public class ServiceDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<FBCB.SGI.Persistence.Entities.Service, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public void Import(FBCB.SGI.Persistence.Entities.Service entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
