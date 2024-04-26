namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class Exposure : SAPRESSC.Core.Persistence.Entities.IHabilitableEntity<System.Guid>
    {
        public Exposure()
        {
            Id = System.Guid.NewGuid();
            CreatedAt = System.DateTime.Now;
            EmployeeCuratorshipBridges = new System.Collections.Generic.List<ExposureEmployeeCuratorshipBridge>();
            Translations = new System.Collections.Generic.List<ExposureTranslation>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public bool Enabled { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Description { get; set; }

        public System.DateTime CreatedAt { get; set; }

        public System.DateTime? StartAt { get; set; }

        public System.DateTime? EndAt { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("GalleryId")]
        public Gallery Gallery { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public Enumerations.GalleryId? GalleryId { get; set; }

        public string FrameBaseColor { get; set; }

        public System.Collections.Generic.List<ExposureEmployeeCuratorshipBridge> EmployeeCuratorshipBridges { get; set; }

        public System.Collections.Generic.List<ExposureTranslation> Translations { get; set; }
    }
}
