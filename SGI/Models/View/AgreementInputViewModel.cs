using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class AgreementInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Agreement, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Number")]
        [Range(1, 1000, ErrorMessage = "RangeValidationErrorMessage")]
        public int Number { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "ReleaseAt")]
        public System.DateTime ReleaseAt { get; set; }

        public Agreement Export()
        {
            var entity = new Agreement();

            Mergue(entity);

            return entity;
        }

        public void Import(Agreement entity)
        {
            Id = entity.Id;
            Number = entity.Number;
            Title = entity.Title;
            Description = entity.Description;
            ReleaseAt = entity.ReleaseAt;
        }

        public void Mergue(Agreement entity)
        {
            entity.Number = Number;
            entity.Title = Title;
            entity.Description = Description;
            entity.ReleaseAt = ReleaseAt;
        }
    }
}
