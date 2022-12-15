namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CraftManifestationGroup : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ParentId")]
        public CraftManifestationGroup Parent { get; set; }
        public System.Guid? ParentId { get; set; }

        public string Description { get; set; }

        public CraftManifestationGroup()
        {
            Id = System.Guid.NewGuid();
        }
    }
}
