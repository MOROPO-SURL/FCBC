namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtisticManifestationGroup : SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ParentId")]
        public ArtisticManifestationGroup Parent { get; set; }
        public System.Guid? ParentId { get; set; }

        public string Description { get; set; }

        public ArtisticManifestationGroup()
        {
            Id = System.Guid.NewGuid();
        }
    }
}
