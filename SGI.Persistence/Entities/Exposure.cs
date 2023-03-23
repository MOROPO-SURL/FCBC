namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class Exposure : SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public Exposure()
        {
            EmployeeCuratorshipBridges = new System.Collections.Generic.List<ExposureEmployeeCuratorshipBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public bool IsEnabled { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Description { get; set; }

        public System.DateTime? StartAt { get; set; }

        public System.DateTime? EndAt { get; set; }

        public System.Collections.Generic.List<ExposureEmployeeCuratorshipBridge> EmployeeCuratorshipBridges { get; set; }
    }
}
