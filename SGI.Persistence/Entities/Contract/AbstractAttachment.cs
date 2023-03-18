namespace MOROPO.FCBC.SGI.Persistence.Entities.Contract
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public abstract class AbstractAttachment
    {
        public AbstractAttachment()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string ContentType { get; set; }

        public long Length { get; set; }

        public bool IsPublic { get; set; }

        public string Description { get; set; }
    }
}
