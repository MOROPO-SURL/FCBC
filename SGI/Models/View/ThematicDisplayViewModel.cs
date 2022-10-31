using MOROPO.FBCB.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ThematicDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Thematic, System.Guid>
    {
        public System.Guid Id { get ; set ; }

        public string Name { get; set; }

        public string Description { get; set; }

        public void Import(Thematic entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
