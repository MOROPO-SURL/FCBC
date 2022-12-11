
namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class ManifestationKind: SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ParentId")]
        public ManifestationKind Parent { get; set; }

        public System.Guid? ParentId { get; set; }

        public ManifestationKind()
        {
            Id = System.Guid.NewGuid();
        }
    }
}
