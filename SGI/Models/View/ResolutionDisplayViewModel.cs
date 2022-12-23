using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ResolutionDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Resolution, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Number")]
        public int NumberResolution { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "ResolutionDate")]
        public System.DateTime ResolutionDate { get; set; }

        public void Import(Persistence.Entities.Resolution entity)
        {
            Id = entity.Id;
            NumberResolution = entity.Number;
            Title = entity.Title;
            Description = entity.Description;
            ResolutionDate = entity.ReleaseAt;
        }
    }
}
