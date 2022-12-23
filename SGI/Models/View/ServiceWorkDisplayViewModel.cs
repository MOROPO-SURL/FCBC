using MOROPO.FCBC.SGI.Persistence.Entities;
using System;
using System.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ServiceWorkDisplayViewModel: SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.ServiceWork, System.Guid>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public ServiceLineDisplayViewModel Line { get; set; }

        public SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities.Image FeatureImage { get; set; }

        public SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities.Image[] Images { get; set; }

        public ServiceTechniqueDisplayViewModel[] Techniques { get; set; }

        public void Import(ServiceWork entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Line = new ServiceLineDisplayViewModel();
            FeatureImage = entity.FeatureImage;
            Images = entity.ImageBridges.Select(bridge => bridge.Image).ToArray();
            Techniques = entity.TechniqueBridges.Select(bridge =>
            {
                var model = new ServiceTechniqueDisplayViewModel();

                model.Import(bridge.Technique);

                return model;
            }).ToArray();

            Line.Import(entity.Line);
        }
    }
}
