namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class Agreement : SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public Agreement()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        public int Number { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public System.DateTime ReleaseAt { get; set; }
    }
}
