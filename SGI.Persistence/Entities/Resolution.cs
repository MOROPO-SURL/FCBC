

namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class Resolution: SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public System.Guid Id { get; set; }
        public string NumberResolution { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Resolution()
        {
            Id = System.Guid.NewGuid();
        }
    }
}
