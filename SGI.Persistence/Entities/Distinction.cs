namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class Distinction : SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public Distinction()
        {
            Id = System.Guid.NewGuid();
        }
    }
}
