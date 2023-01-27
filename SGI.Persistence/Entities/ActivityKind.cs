
namespace MOROPO.FCBC.SGI.Persistence.Entities 
{
    public class ActivityKind : SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        public ActivityKind()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
