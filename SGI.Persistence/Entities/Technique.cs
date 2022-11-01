
namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class Technique : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Description { get; set; }

        public Technique()
        {
            Id = System.Guid.NewGuid();
        }
    }
}
