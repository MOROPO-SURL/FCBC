namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CreationGroupAward : SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public CreationGroupAward()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get ; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("GroupId")]
        public CreationGroup Group { get; set; }

        public System.Guid GroupId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("AwardId")]
        public Award Award { get; set; }
        public System.Guid AwardId { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Context { get; set; }
    }
}
