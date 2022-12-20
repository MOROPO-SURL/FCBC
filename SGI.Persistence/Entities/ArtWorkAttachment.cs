namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtWorkAttachment : Contract.AbstractAttachment, SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("WorkId")]
        public ArtWork Work { get; set; }
        public System.Guid WorkId { get; set; }
    }
}
