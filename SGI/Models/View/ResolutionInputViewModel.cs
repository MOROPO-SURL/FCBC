using MOROPO.FBCB.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models;
using System;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ResolutionInputViewModel : IEntityInputModel<Resolution, System.Guid>
    {
        public Guid Id { get; set; }

        public string NumberResolution { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public System.DateTime ResolutionDate { get; set; }

        public Resolution Export()
        {
            var entity = new Resolution();

            Mergue(entity);

            return entity;
        }

        public void Import(Resolution entity)
        {
            Id = entity.Id;
            NumberResolution = entity.NumberResolution;
            Title = entity.Title;
            Description = entity.Description;
            ResolutionDate = entity.ResolutionDate;
        }

        public void Mergue(Resolution entity)
        {
            entity.NumberResolution = NumberResolution;
            entity.Title = Title;
            entity.Description = Description;
            entity.ResolutionDate = ResolutionDate;
        }
    }
}
