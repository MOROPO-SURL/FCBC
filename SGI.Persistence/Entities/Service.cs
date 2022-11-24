
namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class Service : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public Service()
        {
            Id = System.Guid.NewGuid();
        }
    }
}
