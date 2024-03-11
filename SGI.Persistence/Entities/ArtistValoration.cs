namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtistValoration : SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public ArtistValoration()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("CreatorId")]
        public Artist Creator { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? CreatorId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("CriticId")]
        public Critic Critic { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? CriticId { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Declaration { get; set; }

        public System.DateTime? DeclaredAt { get; set; }
    }
}
