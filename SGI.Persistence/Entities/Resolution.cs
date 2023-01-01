namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class Resolution : SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        public int Number { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public System.DateTime ReleaseAt { get; set; }

        public Resolution()
        {
            Id = System.Guid.NewGuid();
        }
    }
}
