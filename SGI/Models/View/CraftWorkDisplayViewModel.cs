using MOROPO.FCBC.SGI.Persistence.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CraftWorkDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.CraftWork, System.Guid>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }        
        
        public CraftManifestationDisplayViewModel Manifestation { get; set; }

        public SAPRESSC.Middleware.Media.Persistence.Entities.Image FeatureImage { get; set; }

        public SAPRESSC.Middleware.Media.Persistence.Entities.Image[] Images { get; set; }

        public CraftTechniqueDisplayViewModel[] Techniques { get; set; }

        public void Import(CraftWork entity)
        {
            Id = entity.Id;            
            Title = entity.Title;
            Manifestation = new CraftManifestationDisplayViewModel();
            FeatureImage = entity.FeatureImage;
            Images = entity.ImageBridges.Select(bridge => bridge.Image).ToArray();
            Techniques = entity.TechniqueBridges.Select(bridge =>
            {
                var model = new CraftTechniqueDisplayViewModel();

                model.Import(bridge.Technique);

                return model;
            }).ToArray();

            Manifestation.Import(entity.Manifestation);
        }
    }
}
