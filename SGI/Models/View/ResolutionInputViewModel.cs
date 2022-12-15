using MOROPO.FCBC.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ResolutionInputViewModel : IEntityInputModel<Resolution, System.Guid>
    {
        public System.Guid Id { get; set; }

        public int NumberResolution { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

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
