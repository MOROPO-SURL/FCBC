using MOROPO.FCBC.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ResolutionInputViewModel : IEntityInputModel<Resolution, System.Guid>
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

        public ResolutionInputViewModel()
        {
            ResolutionDate = System.DateTime.Now;
        }

        public Resolution Export()
        {
            var entity = new Resolution();

            Mergue(entity);

            return entity;
        }

        public void Import(Resolution entity)
        {
            Id = entity.Id;
            NumberResolution = entity.Number;
            Title = entity.Title;
            Description = entity.Description;
            ResolutionDate = entity.ReleaseAt;
        }

        public void Mergue(Resolution entity)
        {
            entity.Number = NumberResolution;
            entity.Title = Title;
            entity.Description = Description;
            entity.ReleaseAt = ResolutionDate;
        }
    }
}
