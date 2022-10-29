namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class Recognition : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public Recognition()
        {
            Id = System.Guid.NewGuid();
        }
    }
}
