namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class EmployeeAdministrativeRoleBridge
    {
        public System.Guid EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public System.Guid RoleId { get; set; }

        public AdministrativeRole Role { get; set; }
    }
}
