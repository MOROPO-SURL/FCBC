namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ExposureEmployeeCuratorshipBridge
    {
        public System.Guid ExposureId { get; set; }
        public Exposure Exposure { get; set; }

        public System.Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
