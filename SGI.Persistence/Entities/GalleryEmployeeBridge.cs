namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class GalleryEmployeeBridge
    {
        public Enumerations.GalleryId GalleryId { get; set; }
        public Gallery Gallery { get; set; }

        public Employee Employee { get; set; }
        public System.Guid EmployeeId { get; set; }
    }
}
