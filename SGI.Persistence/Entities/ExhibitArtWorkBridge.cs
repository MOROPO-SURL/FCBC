namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ExhibitArtWorkBridge
    {
        public System.Guid ExhibitId { get; set; }
        public Exhibit Exhibit { get; set; }

        public System.Guid ArtWorkId { get; set; }
        public ArtWork ArtWork { get; set; }
    }
}
