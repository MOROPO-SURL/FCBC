namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CreationGroupAttachment : Contract.AbstractAttachment, SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("GroupId")]
        public CreationGroup Group { get; set; }
        public System.Guid GroupId { get; set; }
    }
}
