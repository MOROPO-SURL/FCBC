

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class Material : SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public Material()
        {
            Id = System.Guid.NewGuid();
        }

    }
}
