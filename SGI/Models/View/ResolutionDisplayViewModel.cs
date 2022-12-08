using MOROPO.FBCB.SGI.Persistence.Entities;
using System;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ResolutionDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Resolution, Guid>
    {
        public Guid Id { get; set; }

        public string NumberResolution { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public void Import(Resolution entity)
        {
            Id = entity.Id;
            NumberResolution = entity.NumberResolution;
            Title = entity.Title;
            Description = entity.Description;
        }
    }
}
