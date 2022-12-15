namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtistResolutionLock
    {
        public System.Guid ArtistId { get; set; }

        public Artist Artist { get; set; }

        public System.Guid ResolutionId { get; set; }

        public Resolution Resolution { get; set; }
    }
}
