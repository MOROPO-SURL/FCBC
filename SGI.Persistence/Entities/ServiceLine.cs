namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ServiceLine : SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public ServiceLine()
        {
            Id = System.Guid.NewGuid();
        }

        public System.Collections.Generic.List<ServiceLineTranslation> Translations { get; set; }
    }
}
