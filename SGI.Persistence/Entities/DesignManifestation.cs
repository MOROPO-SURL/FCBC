namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class DesignManifestation : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("GroupId")]
        public DesignManifestationGroup Group { get; set; }
        public System.Guid? GroupId { get; set; }

        public string Description { get; set; }

        public DesignManifestation()
        {
            Id = System.Guid.NewGuid();
        }
    }
}
