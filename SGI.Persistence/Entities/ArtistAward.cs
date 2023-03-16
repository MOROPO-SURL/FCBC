namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtistAward: SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public ArtistAward()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ArtistId")]
        public Artist Artist { get; set; }
        public System.Guid ArtistId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("AwardId")]
        public Award Award { get; set; }
        public System.Guid AwardId { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Context { get; set; }
    }
}
