namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CreationGroupAttachment : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string ContentType { get; set; }

        public long Length { get; set; }

        public bool IsPublic { get; set; }

        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("GroupId")]
        public CreationGroup Group { get; set; }
        public System.Guid GroupId { get; set; }
    }
}
