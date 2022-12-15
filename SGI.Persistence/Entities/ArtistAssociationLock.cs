namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtistAssociationLock
    {
        public System.Guid ArtistId { get; set; }

        public Artist Artist { get; set; }

        public System.Guid AssociationId { get; set; }

        public Association Association { get; set; }
    }
}
