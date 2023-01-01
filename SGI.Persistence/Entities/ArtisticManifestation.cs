namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtisticManifestation : SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("GroupId")]
        public ArtisticManifestationGroup Group { get; set; }
        public System.Guid? GroupId { get; set; }

        public string Description { get; set; }

        public ArtisticManifestation()
        {
            Id = System.Guid.NewGuid();
        }
    }
}
