namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtistAttachment : Contract.AbstractAttachment, SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ArtistId")]
        public Artist Artist { get; set; }
        public System.Guid ArtistId { get; set; }
    }
}
