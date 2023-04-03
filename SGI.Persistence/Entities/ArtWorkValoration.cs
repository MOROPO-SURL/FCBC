namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtWorkValoration : SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("WorkId")]
        public ArtWork Work { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? WorkId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("CriticId")]
        public Critic Critic { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? CriticId { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Declaration { get; set; }

        public System.DateTime? DeclaredAt { get; set; }
    }
}
