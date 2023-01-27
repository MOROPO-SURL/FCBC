using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class AgreementDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Agreement, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Number")]
        public int Number { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "ReleaseAt")]
        public System.DateTime ReleaseAt { get; set; }

        public void Import(Persistence.Entities.Agreement entity)
        {
            Id = entity.Id;
            Number = entity.Number;
            Title = entity.Title;
            Description = entity.Description;
            ReleaseAt = entity.ReleaseAt;
        }
    }
}
