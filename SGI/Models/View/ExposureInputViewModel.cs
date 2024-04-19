using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ExposureInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Exposure, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Enabled")]
        public bool Enabled { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "StartAt")]
        public System.DateTime? StartAt { get; set; }

        [Display(Name = "EndAt")]
        public System.DateTime? EndAt { get; set; }
        
        [Display(Name = "FrameBaseColor")]
        public string FrameBaseColor { get; set; }

        public Exposure Export()
        {
            var entity = new Exposure();

            Mergue(entity);

            return entity;
        }

        public void Import(Exposure entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            StartAt = entity.StartAt;
            EndAt = entity.EndAt;
            Enabled = entity.Enabled;
            FrameBaseColor = entity.FrameBaseColor;
        }

        public void Mergue(Exposure entity)
        {
            entity.Name = Name;
            entity.Enabled = Enabled;
            entity.Description = Description;
            entity.StartAt = StartAt;
            entity.EndAt = EndAt;
            entity.Enabled = entity.Enabled;
            entity.FrameBaseColor = FrameBaseColor;
        }
    }
}
