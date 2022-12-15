namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtistServiceLineBridge
    {
        public System.Guid ArtistId { get; set; }

        public Artist Artist { get; set; }

        public System.Guid LineId { get; set; }

        public ServiceLine Line { get; set; }
    }
}
