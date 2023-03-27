using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ExposureDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Exposure, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Enabled")]
        public bool Enabled { get; set; }

        public System.DateTime CreatedAt { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "StartAt")]
        public System.DateTime? StartAt { get; set; }

        [Display(Name = "EndAt")]
        public System.DateTime? EndAt { get; set; }

        public void Import(Persistence.Entities.Exposure entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Enabled = entity.Enabled;
            CreatedAt = entity.CreatedAt;
            Description = entity.Description;
            StartAt = entity.StartAt;
            EndAt = entity.EndAt;
        }
    }
}
